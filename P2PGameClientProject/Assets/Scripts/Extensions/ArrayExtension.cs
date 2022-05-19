namespace P2PGameClientProject.Extensions {
    public static class ArrayExtension {
        public static T[] GetRange<T>(this T[] array, int startIndex) {
            T[] result = new T[array.Length - startIndex];
            for (int resultId = 0, arrayId = startIndex; resultId < array.Length; resultId++, arrayId++) result[resultId] = array[arrayId];
            return result;
        }
    }
}