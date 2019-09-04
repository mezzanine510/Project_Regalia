using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        public IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        // NOTE: alternate action cancellation method used on death to cancel all action states
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}