using System;

namespace RefrigtzW
{
    [Serializable]
    public class Photo
    {
        private int PhotoNumber = new int();
        private string PhotoName = null;
        private bool PhotoType = new bool();
        public Photo()
        {
        }
        public int PhotoNumberAccsess
        {
            get => PhotoNumber;
            set => PhotoNumber = value;
        }
        public string PhotoNameAccess
        {
            get => PhotoName;
            set => PhotoName = value;
        }
        public bool PhotoTypeAccess
        {
            get => PhotoType;
            set => PhotoType = value;
        }
    }
}
