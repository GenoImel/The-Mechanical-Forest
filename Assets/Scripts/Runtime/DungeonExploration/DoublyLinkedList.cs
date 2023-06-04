namespace Akashic.Runtime.DungeonExploration
{
    internal sealed class DoublyLinkedList
    {
        private DoublyLinkedList nextHallway; 
        private DoublyLinkedList previousHallway; 
        private DungeonRoom room; 

        private enum NextDirection
        {
            Left = 0,
            Up = 1,
            Right = 2,
            Down = 3
        }

        private NextDirection nextDirection;
        
        public DoublyLinkedList(DoublyLinkedList n, DoublyLinkedList p, DungeonRoom r, int dir)
        {
            nextHallway = n;
            previousHallway = p;
            room = r;
            nextDirection = (NextDirection)dir;
        }

        public DoublyLinkedList(int dir, DungeonRoom r = null)
        {
            room = r;
            nextDirection = (NextDirection)dir;
        }

        public DoublyLinkedList getNext()
        {
            return nextHallway;
        }

        public DoublyLinkedList getPrev()
        {
            return previousHallway;
        }

        public DungeonRoom getRoom()
        {
            return room;
        }

        public int getNextDirection()
        {
            return (int)nextDirection;
        }

        public void setNext(DoublyLinkedList n)
        {
            nextHallway = n;
        }

        public void setPrev(DoublyLinkedList p)
        {
            previousHallway = p;
        }

        public void setRoom(DungeonRoom r)
        {
            room = r;
        }

        public void setDir(int dir)
        {
            nextDirection = (NextDirection)dir;
        }

        public bool isNextRoom()
        {
            if (getNext().getRoom() == null)
            {
                return false;
            }
            
            return true;
        }

        public bool isPrevRoom()
        {
            if (getPrev().getRoom() == null)
            {
                return false;
            }
            
            return true;
        }
    }
}