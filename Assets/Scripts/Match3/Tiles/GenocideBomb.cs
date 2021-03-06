﻿using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Match3
{
    public class GenocideBomb : Tile
    {
        [SerializeField] private int scoreMulitplier = 10;
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private float effectDuration;
        [SerializeField] private float freezeDuration;
        [SerializeField] private AudioClip effectSound;

        private MatchEffect onDestroyEffect;

        private void Awake()
        {
            onDestroyEffect = GetComponent<MatchEffect>();
        }

        public override List<Match> OnSwap(Tile other)
        {
            List<MatchElement> elements = new List<MatchElement>();
            var me = grid.FindPos(gameObject).Value;
            elements.Add(grid.MatchElementAt(me.x, me.y));

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var cell = grid.cells[x, y];
                    if (cell.Child.CompareTag(other.tag))
                    {
                        elements.Add(grid.MatchElementAt(x, y));
                    }
                }
            }

            var elementsArray = elements.ToArray();

            onDestroyEffect?.SpawnEffect(elementsArray);


            var matches = new List<Match>();
            matches.Add(new Match(elementsArray, false, scoreMulitplier * elements.Count));
            return matches;
        }
        
    }
}