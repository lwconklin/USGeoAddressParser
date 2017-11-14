// <copyright file="USAStreetAddress.cs" company="Air Osprey">
//     MIT License (MIT). All rights reserved
// </copyright>
// <author>Larry Conklin</author>
// <summary>US Address components class.</summary>

namespace USGeoAddressParser
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class USAStreetAddress 
    {
        public string OriginalAddess 
        {
            get;
            set;
        }

        public string PrettyAddess 
        {
            get;
            set;
        }

        public string Number 
        {
            get;
            set;
        }

        public string PreDirection 
        {
            get;
            set;
        }

        public string Prefix 
        {
            get;
            set;
        }

        public string Name 
        {
            get;
            set;
        }

        public string Type 
        {
            get;
            set;
        }

        public string Suffix 
        {
            get;
            set;
        }

        public string PostDirection 
        {
            get;
            set;
        }

        public string Addr 
        {
            get;
            set;
        }

        public string Unit 
        {
            get;
            set;
        }

        public string City 
        {
            get;
            set;
        }

        public string State 
        {
            get;
            set;
        }

        public string Zip 
        {
            get;
            set;
        }

        public bool POBox 
        {
            get;
            set;
        }

        public string POBoxNumber 
        {
            get;
            set;
        }
    }
}
