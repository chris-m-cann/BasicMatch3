using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Match3
{
    public struct Match
    {
        public MatchElement[] elements;
        public int causeElem;
        public bool canCreateShape;
        public int score;

        public Match(MatchElement[] elems, bool canCreateShape = true, int score = 10)
        {
            elements = elems;
            causeElem = -1;
            this.canCreateShape = canCreateShape;
            this.score = score;
        }

        internal bool OverlapsWith(Match match)
        {
            return elements.Intersect(match.elements).Count() > 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var elem in elements)
            {
                sb.Append($"({elem.i},{elem.j}),");
            }
            return sb.ToString();
        }
    }

    public struct MatchElement
    {
        public int i, j;
        public GameObject tile;

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(i, j);
        }
    }

    public struct MatchComparator : IEqualityComparer<MatchElement>
    {
        public bool Equals(MatchElement x, MatchElement y)
        {
            return x.i == y.i && x.j == y.j && x.tile == y.tile;
        }

        public int GetHashCode(MatchElement obj)
        {
            return obj.GetHashCode() + 31 * obj.i + 31 * obj.j;
        }
    }


    public struct Matches
    {
        public List<Match> matches;
    }

}