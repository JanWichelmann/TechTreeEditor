using IORAMHelper;
using System.Collections.Generic;

namespace GenieLibrary.DataElements
{
	public class TerrainRestriction : IGenieDataElement
	{
		#region Variablen

		/// <summary>
		/// Die Terrainanzahl. Muss vor Lese-/Schreibvorgängen stets aktualisiert werden.
		/// </summary>
		internal static int TerrainCount;

		/// <summary>
		/// Legt für jedes Terrain fest, ob dieses betreten werden kann. Entweder 1 oder 0.
		/// </summary>
		public List<float> TerrainAccessibleFlags;

		/// <summary>
		/// Die Terrain-Grafik-Datensätze.
		/// </summary>
		public List<TerrainPassGraphic> TerrainPassGraphics;

		#endregion Variablen

		#region Funktionen

		public override void ReadData(RAMBuffer buffer)
		{
			TerrainAccessibleFlags = new List<float>(TerrainCount);
			for(int i = 0; i < TerrainCount; ++i)
				TerrainAccessibleFlags.Add(buffer.ReadFloat());

			TerrainPassGraphics = new List<TerrainPassGraphic>(TerrainCount);
			for(int i = 0; i < TerrainCount; ++i)
				TerrainPassGraphics.Add(new TerrainPassGraphic().ReadDataInline(buffer));
		}

		public override void WriteData(RAMBuffer buffer)
		{
			AssertListLength(TerrainAccessibleFlags, TerrainCount);
			TerrainAccessibleFlags.ForEach(e => buffer.WriteFloat(e));

			AssertListLength(TerrainPassGraphics, TerrainCount);
			TerrainPassGraphics.ForEach(e => e.WriteData(buffer));
		}

		#endregion Funktionen

		#region Strukturen

		public class TerrainPassGraphic : IGenieDataElement
		{
			#region Variablen

			/// <summary>
			/// Legt fest, ob auf dem Terrain gebaut werden kann. Entweder 1 oder 0.
			/// </summary>
			public int Buildable;

			/// <summary>
			/// Grafik 1.
			/// </summary>
			public int GraphicID1;

			/// <summary>
			/// Grafik 2.
			/// </summary>
			public int GraphicID2;

			/// <summary>
			/// Anzahl der Grafik-Wiederholungen.
			/// </summary>
			public int ReplicationAmount;

			#endregion Variablen

			#region Funktionen

			public override void ReadData(RAMBuffer buffer)
			{
				Buildable = buffer.ReadInteger();
				GraphicID1 = buffer.ReadInteger();
				GraphicID2 = buffer.ReadInteger();
				ReplicationAmount = buffer.ReadInteger();
			}

			public override void WriteData(RAMBuffer buffer)
			{
				buffer.WriteInteger(Buildable);
				buffer.WriteInteger(GraphicID1);
				buffer.WriteInteger(GraphicID2);
				buffer.WriteInteger(ReplicationAmount);
			}

			#endregion Funktionen
		}

		#endregion Strukturen
	}
}