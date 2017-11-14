// <copyright file="ParseAddress.cs" company="Air Osprey">
//     MIT License (MIT). All rights reserved
// </copyright>
// <author>Larry Conklin</author>
// <summary>US Address Parsing class.</summary>

namespace USGeoAddressParser
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Parse US Address class.
    /// </summary>
    public class ParseAddress
    {
        private static string streetTypesA = "ALLEE|ALLEY|ALLY|ALY|ANEX|ANNEX|ANNX|ANX|ARC|ARCADE|AV|AVE|AVEN|AVENU|AVENUE|AVN|AVNUE|";
        private static string streetTypesB = "BAYOO|BAYOU|BCH|BEACH|BEND|BND|BLF|BLUF|BLUFF|BLUFFS|BOT|BOTTM|BOTTOM|BTM|BLVD|BOUL|BOULEVARD|BOULV|BR|BRANCH|BRNCH|BRDGE|BRG|BRIDGE|BRK|BROOK|BROOKS|BURG|BURGS|BYP|BYPABYPAS|BYPASS|BYPS|";
        private static string streetTypesC = "CAMP|CMP|CP|CANYN|CANYON|CNYN|CYN|CAPE|CPE|CAUSEWAY|CAUSWAY|CSWY|CEN|CENT|CENTER|CENTR|CENTRE|CNTER|CNTR|CTR|CENTERS|CIR|CIRC|CIRCL|CIRCLE|CRCL|CRCLE|CIRCLES|CLF|CLIFF|CLFS|CLIFFS|CLB|CLUB|COMMON|COR|CORNER|CORNERS|CORS|COURSE|CRSE|COURT|CRT|CT|COURTS|CT|COVE|CV|COVES|CK|CR|CREEK|CRK|CRECENT|CRES|CRESCENT|CRESENT|CRSCNT|CRSENT|CRSNT|CREST|CROSSING|CRSSING|CRSSNG|XING|CROSSROAD|CURVE|";
        private static string streetTypesD = "DALE|DL|DAM|DM|DIV|DIVIDE|DV|DVD|DR|DRIV|DRIVE|DRV|DRIVES|";
        private static string streetTypesE = "EST|ESTATE|ESTATES|ESTS|EXP|EXPR|EXPRESS|EXPRESSWAY|EXPW|EXPY|EXT|EXTENSION|EXTN|EXTNSN|EXTENSIONS|EXTS|";
        private static string streetTypesF = "FALL|FALLS|FLS|FERRY|FRRY|FRY|FIELD|FLD|FIELDS|FLDS|FLAT|FLT|FLATS|FLTS|FORD|FRD|FORDS|FOREST|FORESTS|FRST|FORG|FORGE|FRG|FORGES|FORK|FRK|FORKS|FRKS|FORT|FRT|FT|FREEWAY|FREEWY|FRWAY|FRWY|FWY|";
        private static string streetTypesG = "GARDEN|GARDN|GDN|GRDEN|GRDN|GARDENS|GDNS|GRDNS|GATEWAY|GATEWY|GATWAY|GTWAY|GTWY|GLEN|GLN|GLENS|GREEN|GRN|GREENS|GROV|GROVE|GRV|GROVES|";
        private static string streetTypesH = "HARB|HARBOR|HARBR|HBR|HRBOR|HARBORS|HAVENHAVNHVNHEIGHT|HEIGHTS|HGTS|HT|HTS|HIGHWAY|HIGHWY|HIWAY|HIWY|HWAY|HWY|HILL|HL|HILLS|HLS|HLLW|HOLLOW|HOLLOWS|HOLW|HOLWS|";
        private static string streetTypesI = "INLET|INLT|IS|ISLAND|ISLND|ISLANDS|ISLNDS|ISS|ISLE|ISLES|";
        private static string streetTypesJ = "JCT|JCTION|JCTN|JUNCTION|JUNCTN|JUNCTON|JCTNS|JCTS|JUNCTIONS|";
        private static string streetTypesK = "KEY|KY|KEYS|KYS|KNL|KNOL|KNOLL|KNLS|KNOLLS|";
        private static string streetTypesL = "LAKE|LK|LAKES|LKS|LAND|LANDING|LNDG|LNDNG|LA|LANE|LANES|LNLGT|LIGHT|LIGHTS|LF|LOAF|LCK|LOCK|LCKS|LOCKS|LDG|LDGE|LODG|LODGE|LOOP|LOOPS|";
        private static string streetTypesM = "MALL|MANOR|MNR|MANORS|MNRS|MDW|MEADOW|MDWS|MEADOWS|MEDOWS|MEWS|MILL|ML|MILLS|MLS|MISSION|MISSN|MSN|MSSN|MOTORWAY|MNT|MOUNT|MT|MNTAIN|MNTN|MOUNTAIN|MOUNTIN|MTIN|MTN|MNTNS|MOUNTAINS|";
        private static string streetTypesN = "NCK|NECK";
        private static string streetTypesO = "ORCH|ORCHARD|ORCHRD|OVAL|OVL|OVERPASS|";
        private static string streetTypesP = "PARK|PK|PRK|PARKS|PARKWAY|PARKWY|PKWAY|PKWY|PKY|PARKWAYS|PKWYS|PASS|PASSAGE|PATH|PATHS|PIKE|PIKES|PINE|PINES|PNES|PL|PLACE|PLAIN|PLN|PLAINES|PLAINS|PLNS|PLAZA|PLZ|PLZA|POINT|PT|POINTS|PTS|PORT|PRT|PORTS|PRTS|PR|PRAIRIE|PRARIE|PRR|";
        private static string streetTypesR = "RAD|RADIAL|RADIEL|RADL|RAMP|RANCH|RANCHES|RNCH|RNCHS|RAPID|RPD|RAPIDS|RPDS|REST|RST|RDG|RDGE|RIDGE|RDGS|RIDGES|RIV|RIVER|RIVR|RVR|RD|ROAD|RDS|ROADS|ROUTE|ROW|RUE|RUN|";
        private static string streetTypesS = "SHL|SHOAL|SHLS|SHOALS|SHOAR|SHORE|SHR|SHOARS|SHORES|SHRS|SKYWAY|SPG|SPNG|SPRING|SPRNG|SPGS|SPNGS|SPRINGS|SPRNGS|SPUR|SPURS|SQ|SQR|SQRE|SQU|SQUARE|SQRS|SQUARES|STA|STATION|STATN|STN|STRA|STRAV|STRAVE|STRAVEN|STRAVENUE|STRAVN|STRVN|STRVNUE|STREAM|STREME|STRM|ST|STR|STREET|STRT|STREETS|SMT|SUMIT|SUMITT|SUMMIT|";
        private static string streetTypesT = "TER|TERR|TERRACE|THROUGHWAY|TRACE|TRACES|TRCE|TRACKTRACKS|TRAK|TRK|TRKS|TRAFFICWAY|TRFY|TR|TRAIL|TRAILS|TRL|TRLS|TUNEL|TUNL|TUNLS|TUNNEL|TUNNELS|TUNNL|TPK|TPKE|TRNPK|TRPK|TURNPIKE|TURNPK|";
        private static string streetTypesU = "UNDERPASS|UN|UNION|UNIONS|";
        private static string streetTypesV = "VALLEY|VALLY|VLLY|VLY|VALLEYS|VLYS|VDCT|VIA|VIADCT|VIADUCT|VIEW|VW|VIEWS|VWS|VILL|VILLAG|VILLAGE|VILLG|VILLIAGE|VLG|VILLAGES|VLGS|VILLE|VL|VIS|VIST|VISTA|VST|VSTA|";
        private static string streetTypesW = "WALK|WALKS|WALL|WAY|WY|WAYS|WELL|WELLS|WLS|";

        /// <summary>
        /// The driver to lexical parse a us address.
        /// </summary>
        /// <param name="address">Input address.</param>
        /// <returns>USA Street Address.</returns>
        public static USAStreetAddress Parse(string address)
        {
            USAStreetAddress result = new USAStreetAddress();
            StringBuilder sb = new StringBuilder();
            CultureInfo c = CultureInfo.CurrentCulture;
            ClearUSAStreetAddress();
            if (string.IsNullOrEmpty(address))
            {
                return new USAStreetAddress();
            }

            try
            {
                var input = RemoveUnprintableChars(address.ToUpper());
                if (POBoxAddress(address.ToUpper()).POBox.Equals(false))
                {
                    var re2 = new Regex(Digits());
                    var re = new Regex(BuildUSAddressPattern());
                    if (re.IsMatch(input))
                    {
                        var m = re.Match(input);
                        result.OriginalAddess = address;
                        result.Number = m.Groups["Number"].Value;
                        result.Prefix = m.Groups["Prefix"].Value;
                        if (m.Groups["PreDirection"].Value.Length > 2)
                        {
                            result.Prefix = FormatProperCase(m.Groups["PreDirection"].Value);
                        }
                        else
                        {
                            result.Prefix = ToUpperCase(m.Groups["PreDirection"].Value);
                        }

                        if (m.Groups["PostDirection"].Value.Trim().Length > 2)
                        {
                            result.PostDirection = FormatProperCase(m.Groups["PostDirection"].Value);
                        }
                        else
                        {
                            result.PostDirection = ToUpperCase(m.Groups["PostDirection"].Value);
                        }

                        if (m.Groups["Type"].Value.Trim().Length > 2)
                        {
                            result.Type = FormatProperCase(m.Groups["Type"].Value);
                        }
                        else
                        {
                            switch (ToUpperCase(m.Groups["Type"].Value))
                            {
                                case "PL":
                                    result.Type = "Pl";
                                    break;
                                case "DR":
                                    result.Type = "Dr";
                                    break;
                                case "ST":
                                    result.Type = "St";
                                    break;
                                default:
                                    result.Type = ToUpperCase(m.Groups["Type"].Value);
                                    break;
                            }
                        }

                        if (m.Groups["Unit"].Value.Trim().Length > 2)
                        {
                            result.Unit = FormatProperCase(m.Groups["Unit"].Value);
                        }
                        else
                        {
                            result.Unit = ToUpperCase(m.Groups["Unit"].Value);
                        }

                        if (re2.IsMatch(m.Groups["Name"].Value))
                        {
                            result.Name = ToLowerCase(m.Groups["Name"].Value);
                        }
                        else
                        {
                            result.Name = FormatProperCase(m.Groups["Name"].Value);
                        }

                        if (result.POBox.Equals(false))
                        {
                            if (!result.Number.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.Number.Trim());
                            }

                            if (!result.Prefix.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.Prefix.Trim());
                            }

                            if (!result.PreDirection.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.PreDirection.Trim());
                            }

                            if (!result.Name.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.Name.Trim());
                            }

                            if (!result.PostDirection.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.PostDirection.Trim());
                            }

                            if (!result.Type.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.Type.Trim());
                            }

                            if (!result.Unit.Trim().Equals(string.Empty))
                            {
                                sb.Append(" " + result.Unit.Trim());
                            }

                            result.Addr = sb.ToString();

                            // result.Addr = result.Number + " " + result.Prefix + " " + result.PreDirection + " " + result.Name + " " + result.PostDirection + " " + result.Type + " " + result.Unit;
                        }
                    }
                    else
                    {
                        result.OriginalAddess = input;
                    }
                }

                if (result.Addr.Equals(string.Empty))
                {
                    result.Addr = FormatProperCase(result.OriginalAddess);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Build Regex Pattern for US address.
        /// </summary>
        /// <returns>A complete regex statement.</returns>
        private static string BuildUSAddressPattern()
        {
            var pattern = "^" +                                                    //// beginning of string                    
                "(?<Number>\\d+)" +                                                //// 1 or more digits                    
                "(?:\\s+(?<Prefix>" + GetPrefixes() + "))?" +                      //// whitespace + valid prefix (optional)  
                "(?:\\s+(?<PreDirection>" + GetDirection() + "))?" +               //// whitespace + valid direction (optional)    
                "(?:\\s+(?<Name>.*?))" +                                           //// whitespace + anything        
                "(?:" +                                                            //// group (optional) {
                "(?:\\s+(?<PostDirection>" + GetDirection() + "))?" +              ////   whitespace + valid street direction (optional)                    
                "(?:\\s+(?<Type>" + GetStreetTypes() + "))" +                      ////   whitespace + valid street type                    
                "(?:\\s+(?<Unit>.*))?" +                                           ////   whitespace + anything (optional)                    
                ")?" +                                                             //// }                    
                 "$";                                                              //// end of string     
            return pattern;
        }

        /// <summary>
        /// Create PO Box regex pattern.
        /// </summary>
        /// <returns>PO Box regex pattern.</returns>
        private static string POBoxPattern()
        {
            var pattern = "^\\s*((P(OST)?.?\\s*(O(FF(ICE)?)?)?.?\\s+(B(IN|OX))?)|B(IN|OX))";
            return pattern;
        }

        /// <summary>
        /// Create PO Box number regex pattern.
        /// </summary>
        /// <returns>PO Box number regex pattern.</returns>
        private static string POBoxNumberPattern()
        {
            var pattern = "(?<Number>\\d+)";
            return pattern;
        }

        /// <summary>
        /// Regex express for digits.
        /// </summary>
        /// <returns>Regex string expression.</returns>
        private static string Digits()
        {
            return "^?\\d";
        }

        /// <summary>
        /// Get US Postal Office recognize values for street types.
        /// </summary>
        /// <returns>A concatenated string of all street types.</returns>
        private static string GetStreetTypes()
        {
            return streetTypesA + streetTypesB + streetTypesC + streetTypesD + streetTypesE + streetTypesF + streetTypesG + streetTypesH + streetTypesI + streetTypesJ + streetTypesK 
                + streetTypesL + streetTypesM + streetTypesN + streetTypesO + streetTypesP + streetTypesR + streetTypesS + streetTypesT + streetTypesU + streetTypesV + streetTypesW;
        }

        /// <summary>
        /// Changes input string to all lower case characters.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <returns>Lower case value string.</returns>
        private static string ToLowerCase(string value)
        {
            StringBuilder sb = new StringBuilder();
            var s = value.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(char.ToLower(s[i]));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Changes input string to all upper case characters.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <returns>Upper case value string.</returns>
        private static string ToUpperCase(string value)
        {
            StringBuilder sb = new StringBuilder();
            var s = value.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(char.ToUpper(s[i]));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines if address is a PO Box Address.
        /// </summary>
        /// <param name="address">PO Box Address.</param>
        /// <returns>USA Street Address.</returns>
        private static USAStreetAddress POBoxAddress(string address)
        {
            USAStreetAddress result = new USAStreetAddress();
            ClearUSAStreetAddress();
            var re = new Regex(POBoxPattern());
            if (re.IsMatch(address).Equals(true))
            {
                re = new Regex(POBoxNumberPattern());
                if (re.IsMatch(address))
                {
                    var m = re.Match(address);
                    result.OriginalAddess = address;
                    result.POBoxNumber = "P.O. Box " + m.Groups["Number"].Value;
                    result.POBox = true;
                    result.Addr = "P.O. Box " + m.Groups["Number"].Value;
                }
            }

            return result;
        }

        /// <summary>
        /// Replaces commas with empty string from a string.
        /// </summary>
        /// <param name="input">String to search for commas.</param>
        /// <returns>String with no commas.</returns>
        private static string ReplaceComma(string input)
        {
            return input.Replace(",", string.Empty);
        }

        /// <summary>
        /// Determines case in a string.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>Input leaving all upper case title case as is.</returns>
        private static string FormatProperCase(string value)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            value = ProcessWhitespace(value);
            if (!IsAllUpperOrAllLower(value))
            { // leave the CamelCase or Propercase names alone
                return value;
            }
            else
            {
                return ProcessWhitespace(textInfo.ToTitleCase(value.ToLower())); // if all upper case titlecase leaves as is
            }
        }

        /// <summary>
        /// Regex expression for whitespace.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>Trims leading and ending spaces and multiple spaces to single space.</returns>
        private static string ProcessWhitespace(string value)
        {
            string space = " ";
            value = value.Trim().TrimStart().TrimEnd();
            value = Regex.Replace(value, @"\s+", space);
            return value;
        }

        /// <summary>
        /// Determines in string is upper or lower case.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>True or false.</returns>
        private static bool IsAllUpperOrAllLower(string input)
        {
            return input.ToLower().Equals(input) || input.ToUpper().Equals(input);
        }

        /// <summary>
        /// Clears values.
        /// </summary>
        private static void ClearUSAStreetAddress()
        {
            USAStreetAddress result = new USAStreetAddress();
            result.Addr = string.Empty;
            result.City = string.Empty;
            result.Name = string.Empty;
            result.Number = string.Empty;
            result.OriginalAddess = string.Empty;
            result.POBox = false;
            result.POBoxNumber = string.Empty;
            result.PostDirection = string.Empty;
            result.PreDirection = string.Empty;
            result.Prefix = string.Empty;
            result.PrettyAddess = string.Empty;
            result.State = string.Empty;
            result.Type = string.Empty;
            result.Unit = string.Empty;
            result.Zip = string.Empty;
        }

        /// <summary>
        /// Create Regex expression to remove unprintable characters.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Regex expression.</returns>
        private static string RemoveUnprintableChars(string s)
        {
            return Regex.Replace(s, "[\u0000-\u001F]", string.Empty);
        }

        /// <summary>
        ///  Create street prefix regex pattern.
        /// </summary>
        /// <returns>Returns prefix regex pattern.</returns>
        private static string GetPrefixes()
        {
            return "TE|HW|RD|MA|EI|NO|AU|GR|OL|SW|HA|JO|OV|OH|K";
        }

        /// <summary>
        /// Create street direction regex pattern.
        /// </summary>
        /// <returns>Returns string direction pattern.</returns>
        private static string GetDirection()
        {
            return "NW|E|SE|W|SW|S|NE|N|NORTHWEST|EAST|SOUTHEAST|WEST|SOUTHWEST|SOUTH|NORTHEAST|NORTH|EST";
        }
    } // end of class
} // end of namespace
