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
        public void AddMethod_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            var hList = new HList<int?>();

            Assert.Throws<ArgumentNullException>(() => hList.Add(null));
        }

        [Fact]
        public void HList_ShouldBeAccessedByIndex_WhenIndexIsWithinRange()
        {
            var hList = new HList<int>
            {
                10
            };

            var value = hList[0];

            Assert.Equal(10, value);
        }

        [Fact]
        public void HList_ShouldThrowArgumentOutOfRangeException_WhenAccessedByNegativeIndex()
        {
            var hList = new HList<int>
            {
                10
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => hList[-1]);
        }

        [Fact]
        public void HList_ShouldThrowArgumentOutOfRangeException_WhenAccessedByIndexOutOfRange()
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

        // Should delete previous value index!

        [Fact]
        public void GetMethod_ShouldThrowArgumentNullException_ValueIsNull()
        {
            var hList = new HList<int?>();

            Assert.Throws<ArgumentNullException>(() => hList.Get(null));
        }

        [Fact]
        public void GetMethod_ShouldReturnNull_WhenValueDoesNotExist()
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
        public void GetMethod_ShouldReturnListOfValueIndexes_WhenValueExists()
        {
            var hList = new HList<int?>()
            {
                10,
                10
            };

            var indexes = hList.Get(10)!;

            Assert.Equal(2, indexes.Count);
        }

        [Fact]
        public void GetMethod_ShouldReturnCorrectListOfValueIndexes_WhenValueExists()
        {
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            var indexes = hList.Get(10)!;

            Assert.Contains(0, indexes);
            Assert.Contains(2, indexes);
        }

        [Fact]
        public void RemoveMethod_ShouldRemoveAllValueOccurences_WhenValueExists()
        {
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            hList.Remove(10);
            var indexes = hList.Get(10);

            Assert.Null(indexes);
            Assert.DoesNotContain(10, hList);
        }

        [Fact]
        public void RemoveMethod_ShouldThrowArgumentNullException_WhenValueDoesNotExist()
        {
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            Assert.Throws<ArgumentNullException>(() => hList.Remove(16));
        }

        [Fact]
        public void RemoveMethod_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            Assert.Throws<ArgumentNullException>(() => hList.Remove(16));
        }
    }
}