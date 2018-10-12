using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractOnTrigger2D : MonoBehaviour
    {
        public bool EnableReEnter;
        public LayerMask layers;
        public UnityEvent OnEnter, OnExit, OnReEnter;
        public InventoryController.InventoryChecker[] inventoryChecks;

        public bool isEnabled = false;

        protected Collider2D m_Collider;

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            m_Collider = GetComponent<Collider2D>();
            m_Collider.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(!enabled)
                return;
        
            if (layers.Contains(other.gameObject))
            {
                if (EnableReEnter)
                {
                    if (isEnabled)
                    {
                        ExecuteOnReEnter(other);
                        isEnabled = false;
                    }
                    else
                    {
                        ExecuteOnEnter(other);
                        isEnabled = true;
                    }
                }
                else
                {
                    ExecuteOnEnter(other);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(!enabled)
                return;
        
            if (layers.Contains(other.gameObject))
            {
                ExecuteOnExit(other);
            }
        }

        protected virtual void ExecuteOnEnter(Collider2D other)
        {
            OnEnter.Invoke();
            for (int i = 0; i < inventoryChecks.Length; i++)
            {
                inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
            }
        }

        protected virtual void ExecuteOnExit(Collider2D other)
        {
            OnExit.Invoke();
        }

        protected virtual void ExecuteOnReEnter(Collider2D other)
        {
            OnReEnter.Invoke();
            for (int i = 0; i < inventoryChecks.Length; i++)
            {
                inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }
    }
}