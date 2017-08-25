using Net3Migrations.Delegates;

namespace System.Threading
{
    public struct SpinWait
    {
        // The number of step until SpinOnce yield on multicore machine
        const int step = 5;
        const int maxTime = 50;
        static readonly bool isSingleCpu = (Environment.ProcessorCount == 1);

        int ntime;

        public void SpinOnce()
        {
            throw new NotImplementedException();
            //if (isSingleCpu)
            //{
            //    // On a single-CPU system, spinning does no good
            //    Thread.Yield();
            //}
            //else {
            //    if ((ntime = ntime == maxTime ? maxTime : ntime + 1) % step == 0)
            //        Thread.Yield();
            //    else
            //        // Multi-CPU system might be hyper-threaded, let other thread run
            //        Thread.SpinWait(ntime << 1);
            //}
        }

        public static void SpinUntil(Trunc<bool> predicate)
        {
            SpinWait sw = new SpinWait();
            while (!predicate())
                sw.SpinOnce();
        }

        public static bool SpinUntil(Trunc<bool> predicate, TimeSpan ts)
        {
            return SpinUntil(predicate, (int)ts.TotalMilliseconds);
        }

        public static bool SpinUntil(Trunc<bool> predicate, int milliseconds)
        {
            throw new NotImplementedException();
            //SpinWait sw = new SpinWait();
            //Watch watch = Watch.StartNew();

            //while (!predicate())
            //{
            //    if (watch.ElapsedMilliseconds > milliseconds)
            //        return false;
            //    sw.SpinOnce();
            //}

            //return true;
        }

        public void Reset()
        {
            ntime = 0;
        }

        public bool NextSpinWillYield
        {
            get
            {
                return isSingleCpu ? true : ntime % step == 0;
            }
        }

        public int Count
        {
            get
            {
                return ntime;
            }
        }
    }
}