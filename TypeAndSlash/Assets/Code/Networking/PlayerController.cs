using Fusion;
using UnityEngine;
using Code.UI;
using Code.GameEvents;
using CriminalMakers.GameEventHub;
using TMPro;
using UnityEngine.UI;

namespace Code.Networking
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float fallSpeed = 1.5f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BoxCollider2D box;
        [SerializeField] private Words _words;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private SlashController _slashController;
        [SerializeField] private int _damage = 5;
        public Canvas Canvas;
        [Networked] public TickTimer DeflectTimer { get; set; }


        private TypedWordView _wordView;
        private string _currentWord = "";
        private int _progress = 0;

        [Networked, OnChangedRender(nameof(OnEnemyAssigned))]
        public PlayerController Enemy { get; set; }


        [Networked, OnChangedRender(nameof(OnPlayerNameChanged))]
        public NetworkString<_16> PlayerName { get; set; }

        [Networked, OnChangedRender(nameof(OnChangedHealth))]
        public int Health { get; set; }

        [Networked] public NetworkObject DeflectOwner { get; set; }


        public override void Spawned()
        {
            _healthSlider.maxValue = Health;
            _healthSlider.value = Health;
            _playerName.text = PlayerName.ToString();
            if (Object.HasInputAuthority)
            {
                _wordView = FindFirstObjectByType<TypedWordView>(FindObjectsInactive.Include);
                LoadNewWord();

                if (Enemy != null)
                {
                    OnEnemyAssigned();
                }

                GameEventHub.Bind(this);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (Object.HasInputAuthority)
            {
                GameEventHub.Unbind(this);
            }
        }

        private void LoadNewWord()
        {
            _currentWord = _words.GetRandomWord().ToLowerInvariant();
            _progress = 0;

            if (_wordView != null)
            {
                _wordView.SetTargetWord(_currentWord);
            }
        }

        private void OnEnemyAssigned()
        {
            if (_wordView != null && Enemy != null)
            {
                _wordView.SetParent(Enemy.Canvas.transform);
            }
        }

        private void OnChangedHealth()
        {
            if (_healthSlider != null)
            {
                _healthSlider.value = Health;
            }
        }

        private void OnPlayerNameChanged()
        {
            _playerName.text = PlayerName.ToString();
        }

        // ===========================================
        //  TYPING EVENT
        // ===========================================
        [OnGameEvent]
        public void OnKeyboardPressed(OnKeyboardPressedEvent e)
        {
            if (!Object.HasInputAuthority)
            {
                return;
            }

            char c = e.KeyChar;

            if (_progress >= _currentWord.Length)
            {
                return;
            }

            if (_wordView != null)
            {
                _wordView.UpdateTypedText(c);
            }

            if (c == _currentWord[_progress])
            {
                _progress++;

                RPC_RequestDamage(5);
                RPC_MoveTowardsOpponent();

                if (!DeflectTimer.Expired(Runner))
                {
                    RPC_SlashEffect();
                }


                if (_progress == _currentWord.Length)
                {
                    LoadNewWord();
                }
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_RequestDamage(int damage)
        {
            DeflectTimer = TickTimer.CreateFromSeconds(Runner, 0.1f);
            DeflectOwner = Object;
            if (Enemy != null)
            {
                Enemy.RPC_ReceiveDamage(damage, Object);
            }
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RPC_ReceiveDamage(int damage, NetworkObject attacker)
        {
            if (!DeflectTimer.Expired(Runner) &&
                DeflectOwner == Object &&
                attacker != Object)
            {
                RPC_PlayDeflectEffect();
                return;
            }

            Health -= damage;

            if (Health <= 0 && Enemy != null)
            {
                RPC_BroadcastWinner(Enemy.PlayerName.ToString());
            }
        }


        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_BroadcastWinner(string winnerName)
        {
            new OnWinnerDeclared(winnerName).Publish(this);
        }

        // ===========================================
        //  MOVEMENT RPC
        // ===========================================
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_MoveTowardsOpponent()
        {
            if (Enemy == null)
            {
                return;
            }

            Vector2 targetPos = Enemy.transform.position;

            if (Random.value > 0.5f)
            {
                targetPos.x += 1.5f;
            }
            else
            {
                targetPos.x -= 1.5f;
            }

            rb.MovePosition(targetPos);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_PlayDeflectEffect()
        {
            _slashController.PlayAllParticles();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SlashEffect()
        {
            RPC_ClientSlashEffect();
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_ClientSlashEffect()
        {
            if (Enemy == null)
            {
                return;
            }

            _slashController.SetTransform(Enemy.transform.position);
        }

        // ===========================================
        //  FALLING (STATE AUTHORITY)
        // ===========================================
        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority)
            {
                if (!IsGrounded())
                {
                    Vector2 pos = transform.position;
                    pos.y -= fallSpeed * Runner.DeltaTime;
                    rb.MovePosition(pos);
                }
            }
        }

        private bool IsGrounded()
        {
            return box.IsTouchingLayers();
        }
    }
}