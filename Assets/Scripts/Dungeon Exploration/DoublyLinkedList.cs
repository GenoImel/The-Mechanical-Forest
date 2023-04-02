public class DoublyLinkedList
{
    DoublyLinkedList next = null; // Next hallway segment
    DoublyLinkedList prev = null; // Previous hallway segment
    DungeonRoom room = null; // The connecting rooms, null if we are still in hallway segment
    int nextDirection = 0; // Direction you are going to reach next element: 0 = left, 1 = up, 2 = right, 3 = down

    public DoublyLinkedList(DoublyLinkedList n, DoublyLinkedList p, DungeonRoom r, int dir)
    {
        next = n;
        prev = p;
        room = r;
        nextDirection = dir;
    }

    public DoublyLinkedList(int dir, DungeonRoom r = null)
    {
        room = r;
        nextDirection = dir;
    }

    public DoublyLinkedList getNext()
    {
        return next;
    }

    public DoublyLinkedList getPrev()
    {
        return prev;
    }

    public DungeonRoom getRoom()
    {
        return room;
    }

    public int getNextDirection()
    {
        return nextDirection;
    }

    public void setNext(DoublyLinkedList n)
    {
        next = n;
    }

    public void setPrev(DoublyLinkedList p)
    {
        prev = p;
    }

    public void setRoom(DungeonRoom r)
    {
        room = r;
    }

    public void setDir(int d)
    {
        nextDirection = d;
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