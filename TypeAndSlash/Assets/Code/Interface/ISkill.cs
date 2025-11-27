using Code;
using UnityEngine;

namespace Code.Interface
{
    public interface ISkill
    {
        void Initialize(SkillContext context); 
        void OnAcquire();                      
        void OnRemove();                       

        void Tick(float deltaTime);            

        void Activate();                       
        void Deactivate();                     
    }
}


public struct SkillContext
{
    public GameObject Player;
    public SkillHandler SkillHandler;
}