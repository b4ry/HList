using HList;

namespace HListTests
{
    public class HListTests
    {
        /* CANARY TESTS */
        [Fact]
        public void HList_ShouldBeCreatedProperly_WhenInitialized()
        {
            // Arrange
            var hList = new HList<int>();

            // Act & Assert
            Assert.NotNull(hList);
        }

        /* ADD */
        [Fact]
        public void AddMethod_ShouldAddArgument()
        {
            // Arrange
            var hList = new HList<int>();
            
            // Act
            hList.Add(10);

            // Assert
            Assert.Contains(10, hList);
        }

        [Fact]
        public void AddMethod_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            // Arrange
            var hList = new HList<int?>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hList.Add(null));
        }

        [Fact]
        public void AddRange_ShouldAddArguments()
        {
            // Arrange
            var hList = new HList<int>();

            // Act
            hList.AddRange([1,5,4,1]);

            // Assert
            Assert.Equal(4, hList.Count);
            Assert.Contains(1, hList);
            Assert.Contains(5, hList);
            Assert.Contains(4, hList);
        }

        [Fact]
        public void AddRange_ShouldAddArgumentIndexes()
        {
            // Arrange
            var hList = new HList<int>();

            // Act
            hList.AddRange([1, 5, 4, 1]);

            // Assert
            var oneIndexes = hList.GetIndexes(1)!;
            var fiveIndexes = hList.GetIndexes(5)!;
            var fourIndexes = hList.GetIndexes(4)!;

            Assert.Contains(0, oneIndexes);
            Assert.Contains(3, oneIndexes);
            Assert.Contains(1, fiveIndexes);
            Assert.Contains(2, fourIndexes);
        }

        /* INDEX */
        [Fact]
        public void HListValue_ShouldBeAccessibleByItsIndex_WhenIndexIsWithinRange()
        {
            // Arrange
            var hList = new HList<int>
            {
                10
            };

            // Act
            var value = hList[0];

            // Assert
            Assert.Equal(10, value);
        }

        [Fact]
        public void HList_ShouldThrowArgumentOutOfRangeException_WhenAccessedByNegativeIndex()
        {
            // Arrange
            var hList = new HList<int>
            {
                10
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => hList[-1]);
        }

        [Fact]
        public void HList_ShouldThrowArgumentOutOfRangeException_WhenAccessedByIndexOutOfRange()
        {
            // Arrange
            var hList = new HList<int>
            {
                10
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => hList[1]);
        }

        [Fact]
        public void HList_ShouldThrowArgumentNullException_WhenReplacingValueIsNull()
        {
            // Arrange
            var hList = new HList<int?>
            {
                10
            };
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hList[0] = null);
        }

        [Fact]
        public void HListValue_ShouldBeReplaced_WhenNewValueAssignedToItsPosition()
        {
            // Arrange
            var hList = new HList<int>
            {
                10
            };

            // Act
            hList[0] = 1;

            // Assert
            Assert.Equal(1, hList[0]);
        }

        [Fact]
        public void HList_ShouldRemoveOldValueIndex_WhenReplacedWithNewValue()
        {
            // Arrange
            var hList = new HList<int>
            {
                10
            };

            // Act
            hList[0] = 1;

            // Assert
            var indexes = hList.GetIndexes(10);

            Assert.Null(indexes);
        }

        [Fact]
        public void HList_ShouldAddNewValueIndex_WhenReplacedWithNewValue()
        {
            // Arrange
            var hList = new HList<int>
            {
                1,
                10
            };

            // Act
            hList[1] = 1;

            // Assert
            var indexes = hList.GetIndexes(1)!;

            Assert.NotNull(indexes);
            Assert.Equal(2, indexes.Count);
            Assert.Contains(1, indexes);
        }

        /* GET */
        [Fact]
        public void GetMethod_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            // Arrange
            var hList = new HList<int?>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hList.GetIndexes(null));
        }

        [Fact]
        public void GetMethod_ShouldReturnNull_WhenValueDoesNotExist()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                10
            };

            // Act
            var indexes = hList.GetIndexes(12);

            // Assert
            Assert.Null(indexes);
        }

        [Fact]
        public void GetMethod_ShouldReturnListOfValueIndexes_WhenValueExists()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                10
            };

            // Act
            var indexes = hList.GetIndexes(10)!;

            // Assert
            Assert.Equal(2, indexes.Count);
        }

        [Fact]
        public void GetMethod_ShouldReturnCorrectListOfValueIndexes_WhenValueExists()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            // Act
            var indexes = hList.GetIndexes(10)!;

            // Assert
            Assert.Contains(0, indexes);
            Assert.Contains(2, indexes);
        }

        /* REMOVE */
        [Fact]
        public void RemoveMethod_ShouldRemoveAllValueOccurences_WhenValueExists()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            // Act
            hList.Remove(10);

            // Assert
            var indexes = hList.GetIndexes(10);

            Assert.Null(indexes);
            Assert.DoesNotContain(10, hList);
        }

        [Fact]
        public void RemoveMethod_ShouldThrowArgumentNullException_WhenValueDoesNotExist()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hList.Remove(16));
        }

        [Fact]
        public void RemoveMethod_ShouldThrowArgumentNullException_WhenValueIsNull()
        {
            // Arrange
            var hList = new HList<int?>()
            {
                10,
                15,
                10
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => hList.Remove(null));
        }
    }
}