using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core.Factories
{
    public static class BlockFactory
    {
        public static TBlock Create<TBlock>(
            Block parent, 
            By by)
            where TBlock : Block
        {
            return (TBlock)Create(typeof(TBlock), parent, by);
        }

        public static object Create(
            Type blockToCreateType, 
            Block parent,
            By by)
        {
            if (!typeof(Block).IsAssignableFrom(blockToCreateType))
            {
                throw new ArgumentException($"Given type '{blockToCreateType.FullName}' is not assignable to '{typeof(Block).FullName}'.", nameof(blockToCreateType));
            }

            return (Block)ElementFactory.Create(blockToCreateType, parent, by);
        }
    }
}
