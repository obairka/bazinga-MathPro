using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    public interface ISimpleRatingCounter
    {
        int Count(int max, int attemptsCount);
    }

    public class VerySimpleRatingCounter : ISimpleRatingCounter
    {
        
        public int Count(int max, int attemptsCount)
        {
            int maxPenalty = max / 2;
            int possiblePenalty = attemptsCount * 3;
            int penalty = possiblePenalty > maxPenalty ? maxPenalty : possiblePenalty;
            return max - penalty;
        }
    }
}