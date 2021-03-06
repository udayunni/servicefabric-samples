﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace StatelessPiService
{
    using System;
    using System.Text;

    /// <summary>
    /// container for pi estimate
    /// </summary>
    public class Estimate
    {
        /// <summary>
        /// Leibniz numeration value
        /// </summary>
        public const double LeibnizNumerator = 4.0;

        private readonly double floatMax10Exp = 10*((int) Math.Log10(int.MaxValue));

        /// <summary>
        /// gets this pi value
        /// </summary>
        /// <value>a pi value</value>
        public double EstimatedValue { get; set; }

        /// <summary>
        /// gets the iteration count
        /// </summary>
        /// <value>iteration count</value>
        public int IterationCount { get; set; }

        /// <summary>
        /// Follows the Liebniz formula for pi.  ref http://en.wikipedia.org/wiki/Leibniz_formula_for_%CF%80
        /// </summary>
        /// <param name="previousEstimate">previous estimate</param>
        /// <returns>new estimate value</returns>
        public static Estimate PI(Estimate previousEstimate)
        {
            if (previousEstimate == null)
            {
                return new Estimate();
            }

            int prevCount = previousEstimate.IterationCount;
            int fraction = (previousEstimate.IterationCount*2) + 1;

            int count = prevCount + 1;
            double denominator = fraction;
            double newValue;

            if (count%2 != 0)
            {
                newValue = previousEstimate.EstimatedValue + (LeibnizNumerator/denominator);
            }
            else
            {
                newValue = previousEstimate.EstimatedValue - (LeibnizNumerator/denominator);
            }

            Estimate e = new Estimate();
            e.IterationCount = count;
            e.EstimatedValue = newValue;
            return e;
        }

        /// <summary>
        /// compares two <see cref="Estimate"/> values
        /// </summary>
        /// <param name="x">First estimate</param>
        /// <param name="y">Second estimate</param>
        /// <returns>true if <paramref name="x"/> is exactly equal to <paramref name="y"/></returns>
        public static bool Equals(Estimate x, Estimate y)
        {
            if (x == y)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return true;
            }

            return x.IterationCount == y.IterationCount && x.EstimatedValue == y.EstimatedValue;
        }

        /// <summary>
        /// compares this instance to another <see cref="Estimate"/>
        /// </summary>
        /// <param name="x">the <see cref="Estimate"/> to compare with</param>
        /// <returns>true if this <see cref="Estimate"/> is exactly equal to <paramref name="x"/></returns>
        public bool Equals(Estimate x)
        {
            return Equals(this, x);
        }

        /// <summary>
        /// compare this instance to another object
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if this <see cref="Estimate"/> is exactly equal to <paramref name="obj"/></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Estimate)
            {
                return Equals(this, (Estimate) obj);
            }

            return false;
        }

        /// <summary>
        /// hash code for this <see cref="Estimate"/>
        /// </summary>
        /// <returns>new hash code</returns>
        public override int GetHashCode()
        {
            int hash = 23;
            hash = (hash*37) + this.IterationCount;
            hash = (hash*37) + ((int) (this.EstimatedValue*this.floatMax10Exp));
            return hash;
        }

        /// <summary>
        /// contents of this <see cref="Estimate"/> as string
        /// </summary>
        /// <returns>a string representing the contents of this <see cref="Estimate"/></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.IterationCount);
            sb.Append(',');
            sb.Append(this.EstimatedValue);
            return sb.ToString();
        }
    }
}