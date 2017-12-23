using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchingLibrary;
using System;
using System.Linq;

namespace Test.Searching
{
    [TestClass]
    public abstract class SymbolTableTestBase
    {
        protected abstract ISymbolTable<TKey, TValue> CreateSymbolTable<TKey, TValue>() where TKey : IComparable<TKey>;
        private readonly Random _random = new Random();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_KeyIsNull()
        {
            var st = CreateSymbolTable<string, int>();
            st.TryGet(null, out int value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Put_KeyIsNull()
        {
            var st = CreateSymbolTable<string, int>();
            st.Put(null, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Put_ValueIsNull()
        {
            var st = CreateSymbolTable<int, string>();
            st.Put(1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contains_KeyIsNull()
        {
            var st = CreateSymbolTable<string, string>();
            st.Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_KeyIsNull()
        {
            var st = CreateSymbolTable<string, string>();
            st.Delete(null);
        }

        [TestMethod]
        public void PutAndGet()
        {
            var st = CreateSymbolTable<int, int>();

            Assert.IsFalse(st.TryGet(1, out int value));

            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i);
            }

            Assert.AreEqual(100, st.Size);
            Assert.IsFalse(st.IsEmpty);
            Assert.IsFalse(st.TryGet(101, out int value2));
            for (var i = 0; i < 100; i++)
            {
                Assert.IsTrue(st.TryGet(i, out int val));
                Assert.AreEqual(i, val);
            }
        }

        [TestMethod]
        public void PutAndContains()
        {
            var st = CreateSymbolTable<int, int>();
            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }

            Assert.AreEqual(100, st.Size);
            Assert.IsFalse(st.IsEmpty);

            for (var i = 0; i < 100; i++)
            {
                Assert.IsTrue(st.Contains(i));
            }
        }

        [TestMethod]
        public void PutAndDelete()
        {
            var st = CreateSymbolTable<int, int>();
            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }

            for (var i = 0; i < 100; i++)
            {
                Assert.IsTrue(st.Contains(i));
                st.Delete(i);
                Assert.IsFalse(st.Contains(i));
            }

            Assert.IsTrue(st.IsEmpty);
            Assert.AreEqual(0, st.Size);
        }

        [TestMethod]
        public void MaxAndMin()
        {
            var st = CreateSymbolTable<int, int>();
            Assert.IsFalse(st.TryGetMax(out int keyMax));
            Assert.IsFalse(st.TryGetMin(out int keyMin));
            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }
            Assert.IsTrue(st.TryGetMax(out int keyMax2));
            Assert.AreEqual(99, keyMax2);
            Assert.IsTrue(st.TryGetMin(out int keyMin2));
            Assert.AreEqual(0, keyMin2);
        }

        [TestMethod]
        public void DeleteMaxAndMin()
        {
            var st = CreateSymbolTable<int, int>();
            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }
            Assert.IsTrue(st.Contains(99));
            Assert.IsTrue(st.Contains(0));
            st.DeleteMax();
            st.DeleteMin();
            Assert.IsFalse(st.Contains(99));
            Assert.IsFalse(st.Contains(0));

            Assert.IsTrue(st.Contains(98));
            Assert.IsTrue(st.Contains(1));
            st.DeleteMax();
            st.DeleteMin();
            Assert.IsFalse(st.Contains(98));
            Assert.IsFalse(st.Contains(1));
        }

        [TestMethod]
        public void FloorAndCeiling()
        {
            var st = CreateSymbolTable<int, int>();
            Assert.IsFalse(st.TryGetFloor(10, out int keyFloor1));
            Assert.IsFalse(st.TryGetCeiling(10, out int keyCeiling1));

            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }

            Assert.IsFalse(st.TryGetFloor(0, out int keyFloor2));
            Assert.IsFalse(st.TryGetCeiling(99, out int keyCeiling2));
            Assert.IsTrue(st.TryGetFloor(88, out int keyFloor3));
            Assert.AreEqual(87, keyFloor3);
            Assert.IsTrue(st.TryGetCeiling(88, out int keyCeiling3));
            Assert.AreEqual(89, keyCeiling3);
        }

        [TestMethod]
        public void PutAndGet1000()
        {
            PutAndGet(1000);
        }

        [TestMethod]
        public void PutAndGet10000()
        {
            PutAndGet(10000);
        }

        [TestMethod]
        public void Keys()
        {
            var st = CreateSymbolTable<int, int>();
            for (var i = 0; i < 100; i++)
            {
                st.Put(i, i * 100);
            }

            var keys = st.Keys.ToArray();
            for (var i = 0; i < 100; i++)
            {
                Assert.IsTrue(keys.Contains(i), $"{i} is not found.");
            }
        }

        private void PutAndGet(int n)
        {
            var st = CreateSymbolTable<int, int>();
            var keys = GenerateIntArray(n);
            foreach (var i in keys)
            {
                st.Put(i, i);
            }

            foreach (var i in keys)
            {
                Assert.IsTrue(st.TryGet(i, out int val));
                Assert.AreEqual(i, val);
            }
        }

        private int[] GenerateIntArray(int arrayLength)
        {
            var array = new int[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                var val = _random.Next(arrayLength * 100);
                array[i] = val;
            }
            return array;
        }
    }
}
