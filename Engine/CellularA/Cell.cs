namespace MonoEngine.CellularA
{

    struct Cell<T>
    {
        public T state;

        public int ticksOld;

        public Cell(T _state)
        {
            state = _state;
            ticksOld = 0;
        }
    }
}
