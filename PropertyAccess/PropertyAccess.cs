using NetEti.Globals;

namespace NetEti.ApplicationEnvironment
{
    /// <summary>
    /// Zugriffe auf Properties der aktuellen Anwendung (AppSettings).<br></br>
    /// Nutzt System.Reflection, implementiert IGetStringValue.
    /// </summary>
    /// <remarks>
    /// File: PropertyAccess.cs<br></br>
    /// Autor: Erik Nagel, NetEti<br></br>
    ///<br></br>
    /// 11.01.2018 Erik Nagel: erstellt<br></br>
    /// </remarks>
    public class PropertyAccess : IGetStringValue
    {

        #region public members

        #region IGetStringValue Members

        /// <summary>
        /// Liefert genau einen Wert zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, wird defaultValue zurückgegeben.
        /// </summary>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValue">Das default-Ergebnis (string)</param>
        /// <returns>Der Ergebnis-String</returns>
        public string? GetStringValue(string key, string? defaultValue)
        {
            string? rtn = defaultValue;
            if (this._loadedProperties.ContainsKey(key) && this._loadedProperties[key] != null)
            {
                rtn = this._loadedProperties[key]?.ToString();
            }
            return rtn;
        }

        /// <summary>
        /// Liefert ein string-Array zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, wird defaultValue zurückgegeben.
        /// Liefert nur einen Einzelwert als Array verpackt, muss ggf. spaeter
        /// erweitert werden.
        /// </summary>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValues">Das default-Ergebnis (string[])</param>
        /// <returns>Das Ergebnis-String-Array</returns>
        public string?[]? GetStringValues(string key, string?[]? defaultValues)
        {
            string? rtn = GetStringValue(key, null);
            if (rtn != null)
            {
                return new string[] { rtn };
            }
            else
            {
                return defaultValues;
            }
        }

        /// <summary>
        /// Liefert einen beschreibenden Namen dieses StringValueGetters,
        /// z.B. Name plus ggf. Quellpfad.
        /// </summary>
        public string Description { get; set; }

        #endregion IGetStringValue Members

        /// <summary>
        /// Liefert genau einen Wert zu einem Key. Wenn es keinen Wert zu dem
        /// Key gibt, oder der Wert nicht vom Typ T ist, wird defaultValue zurückgegeben.
        /// </summary>
        /// <typeparam name="T">Der gewünschte Rückgabe-Typ</typeparam>
        /// <param name="key">Der Zugriffsschlüssel (string)</param>
        /// <param name="defaultValue">Das default-Ergebnis vom Typ T</param>
        /// <returns>Wert zum key in den Rückgabe-Typ gecastet</returns>
        /// <exception cref="InvalidCastException">Typecast-Fehler</exception>
        public T? GetValue<T>(string key, T? defaultValue)
        {
            object? rtn = defaultValue;
            if (this._loadedProperties.ContainsKey(key))
            {
                rtn = this._loadedProperties[key];
                if (typeof(T) != rtn?.GetType())
                {
                    rtn = defaultValue;
                }
            }
            return (T?)rtn;
        }

        /// <summary>
        /// Konstruktor - lädt alle Properties des übergebenen Objekts "owner" (i.d.R. AppSettings).
        /// </summary>
        /// <param name="owner">Objekt, dessen Properties ausgewertet werden sollen (i.d.R. AppSettings).</param>
        public PropertyAccess(object owner)
        {
            this._owner = owner;
            this.Description = "Properties";
            this._loadedProperties = new Dictionary<string, object?>();
            this.Load();
        }

        #endregion public members

        #region private members

        private object _owner;

        private Dictionary<string, object?> _loadedProperties;

        private void Load()
        {
            this._loadedProperties.Clear();
            foreach (var prop in this._owner.GetType().GetProperties())
            {
                this._loadedProperties.Add(prop.Name, prop.GetValue(this._owner, null));
            }
        }

        #endregion private members

    }
}
