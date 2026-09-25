namespace GeneticSearch
{
    /// <summary>
    /// Хранит данные об одном белке: его название, организм-носитель
    /// и цепочку аминокислот (уже раскодированную, без RLE-сжатия).
    /// </summary>
    struct GeneticData
    {
        public string protein;   // название белка
        public string organism;  // название организма
        public string amino_acids; // цепочка аминокислот
    }
}
