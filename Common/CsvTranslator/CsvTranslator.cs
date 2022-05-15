using Common.Extensions;
using System.Text;

namespace Common.CsvTranslator
{
    public class CsvTranslator
    {
        #region Make Csv

        public string ListObjToCsv<T>(List<T> objects)
        {
            var header = MakeCsvHeader<T>();
            var properties
                = typeof(T).GetProperties()
                    .Select(p => p.Name)
                    .ToList();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(header + Environment.NewLine);
            foreach (var obj in objects)
            {
                stringBuilder.Append(ObjToCsvLine(obj, properties) + Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        private string ObjToCsvLine<T>(T obj, List<string> props)
        {
            var stringBuilder = new StringBuilder();
            foreach (var prop in props)
            {
                var value = obj.GetPropertyValue(prop);
                stringBuilder.Append(obj.GetPropertyValue(prop) + ";");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        private string MakeCsvHeader<T>()
        {
            return string.Join(";", typeof(T).GetProperties().Select(p => p.Name).ToList());
        }

        #endregion

        #region Make object 

        public T CsvLineToObject<T>(string scvValues, string scvHeader)
            where T : class
        {
            var obj = Activator.CreateInstance(typeof(T));
            if (obj is T)
            {
                var properties = typeof(T).GetProperties();
                var parsedHeader = ParseCsvLine(scvHeader);
                // check if equals
                var parsedValues = ParseCsvLine(scvValues);

                for (int i = 0; i < parsedHeader.Count(); i++)
                {
                    obj.SetPropertyValue(parsedHeader[i], parsedValues[i]);
                }

                return (T)obj;
            }

            throw new Exception("Bad type");
        }

        private List<string> ParseCsvLine(string scvLine)
        {
            return scvLine.Split(new char[] { ';' })
                    .ToList();
        }

        #endregion
    }
}
