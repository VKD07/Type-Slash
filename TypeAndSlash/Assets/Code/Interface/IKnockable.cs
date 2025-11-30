using Vector3 = UnityEngine.Vector3;

namespace Code.Interface
{
    public interface IKnockable
    {
        public void KnockBack(Vector3 dir, float strength);
    }
}