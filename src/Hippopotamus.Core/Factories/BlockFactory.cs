using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core.Factories
{
    public static class BlockFactory
    {
        public static TBlock Create<TBlock>(
            IBlock parent,
            By by)
            where TBlock : IBlock
        {
            return (TBlock)Create(typeof(TBlock), parent, by);
        }

        public static object Create(
            Type blockToCreateType,
            IBlock parent,
            By by)
        {
            if (!typeof(IBlock).IsAssignableFrom(blockToCreateType))
            {
                throw new ArgumentException($"Given type '{blockToCreateType.FullName}' is not assignable to '{typeof(IBlock).FullName}'.", nameof(blockToCreateType));
            }

            return (IBlock)ElementFactory.Create(blockToCreateType, parent, by);
        }
    }
}
