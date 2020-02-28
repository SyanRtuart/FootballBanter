﻿using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates
{
    public class Player : Entity
    {
        public string Name { get; private set; }
        public int Number { get;  private set; }

        public Player(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}
