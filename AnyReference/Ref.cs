namespace AnyReference
{
    using System;

    public struct Ref<T>
    {
        public T Value
        {
            get { return _get(); }
            set { _set(value); }
        }

        private readonly Func<T> _get;
        private readonly Action<T> _set;

        public Ref(Func<T> get, Action<T> set)
        {
            _get = get;
            _set = set;
        }
    }
}
