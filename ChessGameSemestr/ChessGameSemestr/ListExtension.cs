using System.Collections.Generic;
using System.Drawing;

namespace ChessGameSemestr
{
    public static class ListExtension
    {
        public static Figure FindAndGetByPoint(this List<Figure> list, Point p)
        {
            foreach (var e in list)
                if (e.Position.Equals(p))
                    return e;
            return null;
        }

        public static Figure GetWhiteKing(this List<Figure> list)
        {
            foreach (var e in list)
                if (e.Type == "King")
                    if (e.isWhite)
                        return e;
            return null;
        }

        public static Figure GetBlackKing(this List<Figure> list)
        {
            foreach (var e in list)
                if (e.Type == "King")
                    if (!e.isWhite)
                        return e;
            return null;
        }

        public static Color WhatColorNow(this List<PartOfBoard> list, Point p)
        {
            foreach(var e in list)
                if (e.Point.Equals(p))
                    return e.Color;
            return Color.Empty;
        }
    }
}
