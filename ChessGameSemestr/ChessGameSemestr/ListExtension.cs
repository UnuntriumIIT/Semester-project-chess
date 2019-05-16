using System.Collections.Generic;
using System.Drawing;

namespace ChessGameSemestr
{
    public static class ListExtension
    {
        public static Figure FindAndGetByPoint(this List<Figure> list, Point p)
        {
            Figure output = null;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Position.Equals(p))
                {
                    output = list[i];
                    break;
                }
            }
            return output;
        }
    }
}
