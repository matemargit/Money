﻿using Money.Data.Dto;

namespace Money.Data
{
    public interface IWriteOnlyData
    {
        ulong Insert(double ammount, string text, DateOnly date);
        int Import(IEnumerable<SerializableSpending> toImport);
    }
}