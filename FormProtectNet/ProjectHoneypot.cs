using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace FormProtectNet
{
    /// <summary>
    /// This class queries Project Honeypot's Http:BL IP address blacklist.
    /// See http://projecthoneypot.org/ for more information.
    /// Implemented by Balint Farkas (balint.farkas@windowslive.com).
    /// This is free software, use for whatever purpose you want.
    /// </summary>
    public static class ProjectHoneypot
    {
        /// <summary>
        /// Postfix to be used for queries to Http:BL.
        /// </summary>
        private const string httpBlDomain = "dnsbl.httpbl.org";

        /// <summary>
        /// Holds possible return values for a Http:BL lookup.
        /// </summary>
        public enum HttpBlLookupResult
        {
            /// <summary>
            /// The lookup failed
            /// </summary>
            ErrorOccurred,
            /// <summary>
            /// The host was found on the blacklist
            /// </summary>
            OnBlacklist,
            /// <summary>
            /// The host was not found on the blacklist
            /// </summary>
            NotOnBlacklist
        }

        /// <summary>
        /// Looks up the IP on Project Honeypot's Http:BL blacklist.
        /// </summary>
        public static HttpBlLookupResult Lookup(string ip, string accessKey)
        {
            try
            {
                //Create the query
                string reversedIp = ReverseIp(ip);
                string query = string.Format("{0}.{1}.{2}", accessKey, reversedIp, httpBlDomain);

                //Perform the query.
                //As per the API specification, the query gives back a NXDOMAIN result
                //when an address is not on the blacklist.
                //This translates to a .NET SocketException with SocketError.HostNotFound error code.
                string queryResult = null;
                try
                {
                    queryResult = Dns.GetHostAddresses(query).First().ToString();
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.HostNotFound)
                    {
                        return HttpBlLookupResult.NotOnBlacklist;
                    }
                }

                //Interpret the result
                string[] octets = queryResult.Split('.');

                //As per the API specifications, the first octet has to be 127.
                //If it isn't 127, some kind of error has occurred.
                if (octets[0] != "127")
                {
                    return HttpBlLookupResult.ErrorOccurred;
                }

                //If we received a valid DNS result and the request was not in error,
                //the IP is on the blacklist
                return HttpBlLookupResult.OnBlacklist;
            }
            catch (Exception)
            {
                return HttpBlLookupResult.ErrorOccurred;
            }
        }

        /// <summary>
        /// Octet-reverses the given IPv4 address.
        /// Assumes the input string to be in the correct format.
        /// </summary>
        public static string ReverseIp(string input)
        {
            string[] octets = input.Split('.');
            return string.Format("{0}.{1}.{2}.{3}", octets[3], octets[2], octets[1], octets[0]);
        }
    }
}
