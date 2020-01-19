using System.Linq;
using System.Text;
using UnityEngine;

namespace Match3
{
    public static class MatchShapes
    {
        // I feel like there is a clever way of doing this having a cutom inspector for adding these in and 
        // rotating matches so I only need to do one per shape but this is simple and works and wont need extension so not sure if is worth it
        
        private const bool o = true;
        private const bool x = false;
        private static bool[,,] shapes = {
        // threes
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },
        // fours
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, o, x}
        },
        {
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },
        // fives
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, o, o}
        },
        {
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },
        // Ts
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, o, o, x, x},
            {o, x, x, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, o, x, x, x},
            {x, o, x, x, x},
            {o, o, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, o, x, x},
            {o, o, o, x, x},
            {x, x, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, x, x},
            {x, o, x, x, x},
            {x, o, x, x, x}
        },
        // T4s
        {
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, o, o, x, x},
            {o, x, x, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x},
            {o, o, o, o, x}
        },
        {
            {x, x, x, x, x},
            {x, x, o, x, x},
            {o, o, o, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, o, x},
            {x, o, x, x, x},
            {x, o, x, x, x}
        },
        {
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, o, o, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, o, x, x, x},
            {x, o, x, x, x},
            {o, o, o, o, x}
        },
        {
            {x, x, x, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x},
            {o, o, o, x, x},
            {x, x, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, o, x},
            {x, x, o, x, x},
            {x, x, o, x, x}
        },
        // T5s
        {
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, o, o, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x},
            {o, o, o, o, o}
        },
        {
            {x, x, o, x, x},
            {x, x, o, x, x},
            {o, o, o, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, o, o},
            {x, x, o, x, x},
            {x, x, o, x, x}
        },
        // Ls
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x},
            {o, o, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x},
            {o, o, o, x, x}
        },
        {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, x, x},
            {x, x, o, x, x},
            {x, x, o, x, x}
        },
                                                                                                                                                                                                                                                                {
            {x, x, x, x, x},
            {x, x, x, x, x},
            {o, o, o, x, x},
            {o, x, x, x, x},
            {o, x, x, x, x}
        },

    };


        public static ShapeType? FindShape(Match match)
        {
            // map to a 2D grid of bools to check against our static array of match shapes
            var xMin = match.elements.Min(it => it.i);
            var yMin = match.elements.Min(it => it.j);


            var shape = match.elements.Select(it => { it.i -= xMin; it.j -= yMin; return it; });

            var bools = new bool[5, 5];

            foreach (var s in shape)
            {
                bools[s.i, 4 - s.j] = true;
            }

            //LogMatchGrid(bools);

            return Lookup(bools);
        }

        private static void LogMatchGrid(bool[,] bools)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    sb.Append(bools[j, i] ? "o" : "x");
                }
                sb.Append("\n");
            }
            Debug.Log(sb.ToString());
        }

        private static ShapeType? Lookup(bool[,] match)
        {
            for (int i = 0; i < shapes.GetLength(0); i++)
            {
                var isMatch = true;
                for (int y = 0; isMatch && y < shapes.GetLength(1); y++)
                {
                    for (int x = 0; isMatch && x < shapes.GetLength(2); x++)
                    {
                        if (shapes[i, y, x] != match[x, y])
                        {
                            isMatch = false;
                        }
                    }
                }

                if (isMatch)
                {
                    return GetMatchType(i);
                }
            }

            return null;
        }

        private static ShapeType GetMatchType(int i)
        {
            ShapeType[] matches =
            {
            ShapeType.Three, ShapeType.Three,
            ShapeType.Four, ShapeType.Four,
            ShapeType.Five, ShapeType.Five,
            ShapeType.T3, ShapeType.T3, ShapeType.T3, ShapeType.T3,
            ShapeType.T4, ShapeType.T4, ShapeType.T4, ShapeType.T4, ShapeType.T4, ShapeType.T4, ShapeType.T4, ShapeType.T4,
            ShapeType.T5, ShapeType.T5, ShapeType.T5, ShapeType.T5,
            ShapeType.L, ShapeType.L, ShapeType.L, ShapeType.L
        };
            return matches[i];
        }
    }

    public enum ShapeType
    {
        Three, Four, Five, T3, T4, T5, L
    }

}