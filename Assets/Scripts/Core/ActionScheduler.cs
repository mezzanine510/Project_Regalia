using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        // NOTE: alternate action cancellation method used on enemy death to cancel all actions states
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}