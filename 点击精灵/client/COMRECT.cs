namespace client
{
    using System;

    public class COMRECT
    {
        public int bottom;
        public int left;
        public int right;
        public int top;

        public COMRECT()
        {
        }

        public COMRECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }
}

