namespace Formulas
{
    internal class StackBoolean
    {
        private readonly bool[] Stack = new bool[100];
        private int Top = 99;
        public void Push(bool A)
        {
            if (isFull())
            {
                return;
            }

            Stack[Top] = A;
            Top--;

        }
        public bool Pop()
        {
            if (isEmpty())
            {
                return false;
            }

            Top++;
            return Stack[Top];

        }

        private bool isEmpty()
        {
            if (Top == 99)
            {
                return true;
            }

            return false;
        }

        private bool isFull()
        {
            if (Top == 0)
            {
                return true;
            }

            return false;
        }
    }
}


