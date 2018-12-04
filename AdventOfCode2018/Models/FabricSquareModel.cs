﻿namespace AdventOfCode2018.Models
{
    public class FabricSquareModel
    {
        public int ClaimId { get; }
        
        public int XCoordinate { get; }
        
        public int YCoordinate { get; }
        
        public int Width { get; }
        
        public int Height { get; }

        public FabricSquareModel(int claimId, int xCoordinate, int yCoordinate, int width, int height)
        {
            ClaimId = claimId;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Width = width;
            Height = height;
        }
    }
}