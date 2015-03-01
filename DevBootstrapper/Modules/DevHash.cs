using System.Text;

namespace DevBootstrapper.Modules {
    public static class DevHash {
        /// <summary>
        ///     Checks nulls and returns only codes for existing ones.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Get(params object[] o) {
            var _sb = new StringBuilder(10);
            _sb.Clear();

            for (var i = 0; o[i] != null && i < o.Length; i++) {
                _sb.AppendLine(o[i].GetHashCode() + "_");
            }
            return _sb.ToString();
        }
    }
}