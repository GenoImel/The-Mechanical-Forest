namespace Akashic.Runtime.Utilities
{
    internal sealed class DoublyLinkedList<TDoublyLinkedList> where TDoublyLinkedList : IDoublyLinkedList
    {
        private DoublyLinkedList<TDoublyLinkedList> nextNode; 
        private DoublyLinkedList<TDoublyLinkedList> previousNode; 
        private TDoublyLinkedList node; 

        private FourDirections nextDirection;
        
        public DoublyLinkedList(DoublyLinkedList<TDoublyLinkedList> next, DoublyLinkedList<TDoublyLinkedList> previous, TDoublyLinkedList r, int direction)
        {
            nextNode = next;
            previousNode = previous;
            node = r;
            nextDirection = (FourDirections) direction;
        }

        public DoublyLinkedList(int direction, TDoublyLinkedList r)
        {
            node = r;
            nextDirection = (FourDirections) direction;
        }

        public DoublyLinkedList<TDoublyLinkedList> GetNext()
        {
            return nextNode;
        }

        public DoublyLinkedList<TDoublyLinkedList> GetPrev()
        {
            return previousNode;
        }

        public TDoublyLinkedList GetRoom()
        {
            return node;
        }

        public int GetNextDirection()
        {
            return (int) nextDirection;
        }

        public void SetNext(DoublyLinkedList<TDoublyLinkedList> next)
        {
            nextNode = next;
        }

        public void SetPrev(DoublyLinkedList<TDoublyLinkedList> previous)
        {
            previousNode = previous;
        }

        public void SetRoom(TDoublyLinkedList r)
        {
            node = r;
        }

        public void SetDir(int direction)
        {
            nextDirection = (FourDirections) direction;
        }

        public bool IsNextRoom()
        {
            if (GetNext().GetRoom() == null)
            {
                return false;
            }
            
            return true;
        }

        public bool IsPrevRoom()
        {
            if (GetPrev().GetRoom() == null)
            {
                return false;
            }
            
            return true;
        }
    }
}