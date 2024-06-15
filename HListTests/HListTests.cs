using HList;

namespace HListTests
{
    public class HListTests
    {
        [Fact]
        public void HList_ShouldBeCreatedProperly_WhenInitialized()
        {
            var hList = new HList<int>();

            Assert.NotNull(hList);
        }

        [Fact]
        public void AddMethod_ShouldAddArgument_WhenCalledWithThisArgument()
        {
            var hList = new HList<int>();
            hList.Add(10);

            Assert.Contains(10, hList);
        }

        [Fact]
        public void AddMethod_ShouldThrowArgumentNullException_WhenCalledWithNull()
        {
            var hList = new HList<int?>();

            Assert.Throws<ArgumentNullException>(() => hList.Add(null));
        }

        [Fact]
        public void HListValue_ShouldBeAccessedByItsIndex_WhenIndexWithinRange()
        {
            var hList = new HList<int>
            {
                10
            };

            var value = hList[0];

            Assert.Equal(10, value);
        }

        [Fact]
        public void HListValue_ShouldThrowArgumentOutOfRangeException_WhenAccessedByNegativeIndex()
        {
            var hList = new HList<int>
            {
                10
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => hList[-1]);
        }

        [Fact]
        public void HListValue_ShouldThrowArgumentOutOfRangeException_WhenAccessedByIndexOutOfRange()
        {
            var hList = new HList<int>
            {
                10
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => hList[1]);
        }

        [Fact]
        public void HListValue_ShouldBeReplaced_WhenNewValueAssignedToItsPosition()
        {
            var hList = new HList<int>
            {
                10
            };

            hList[0] = 1;

            Assert.Equal(1, hList[0]);
        }

        [Fact]
        public void GetMethod_ShouldThrowArgumentNullException_WhenCalledWithNull()
        {
            var hList = new HList<int?>();

            Assert.Throws<ArgumentNullException>(() => hList.Get(null));
        }

        [Fact]
        public void GetMethod_ShouldReturnNull_WhenCalledWithMissingArgument()
        {
            var hList = new HList<int?>()
            {
                10,
                10
            };

            var indexes = hList.Get(12);

            Assert.Null(indexes);
        }

        [Fact]
        public void GetMethod_ShouldReturnListOfValueIndexes_WhenCalledWithThisValue()
        {
            var hList = new HList<int?>()
            {
                10,
                10
            };

            var indexes = hList.Get(10);

            Assert.Equal(2, indexes!.Count);
        }
    }
}