#region Auto-generated classes for requerimento database on 2010-10-29 18:33:38Z

//
//  ____  _     __  __      _        _
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from requerimento on 2010-10-29 18:33:38Z
// Please visit http://linq.to/db for more information

#endregion

using System;
using System.Data;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Reflection;
using DbLinq.Data.Linq;
using DbLinq.Vendor;
using System.ComponentModel;

namespace RCPJ
{
	public partial class ReQUERimeNto : DataContext
	{
		public ReQUERimeNto(IDbConnection connection)
		: base(connection, new DbLinq.MySql.MySqlVendor())
		{
		}

		public ReQUERimeNto(IDbConnection connection, IVendor vendor)
		: base(connection, vendor)
		{
		}

		public Table<A001AtIViDade> A001AtIViDade { get { return GetTable<A001AtIViDade>(); } }
		public Table<A002AtO> A002AtO { get { return GetTable<A002AtO>(); } }
		public Table<A003EventO> A003EventO { get { return GetTable<A003EventO>(); } }
		public Table<A004UF> A004UF { get { return GetTable<A004UF>(); } }
		public Table<A005MuNiCipIo> A005MuNiCipIo { get { return GetTable<A005MuNiCipIo>(); } }
		public Table<A006NatureZAJuridicA> A006NatureZAJuridicA { get { return GetTable<A006NatureZAJuridicA>(); } }
		public Table<A007SituAcAO> A007SituAcAO { get { return GetTable<A007SituAcAO>(); } }
		public Table<A008StatusAtUAL> A008StatusAtUAL { get { return GetTable<A008StatusAtUAL>(); } }
		public Table<A009ConDiCaO> A009ConDiCaO { get { return GetTable<A009ConDiCaO>(); } }
		public Table<A010TipODocumentO> A010TipODocumentO { get { return GetTable<A010TipODocumentO>(); } }
		public Table<A011Porte> A011Porte { get { return GetTable<A011Porte>(); } }
		public Table<A012EstAdoCivil> A012EstAdoCivil { get { return GetTable<A012EstAdoCivil>(); } }
		public Table<A013RegimeBenS> A013RegimeBenS { get { return GetTable<A013RegimeBenS>(); } }
		public Table<A014EmAnCipAcAO> A014EmAnCipAcAO { get { return GetTable<A014EmAnCipAcAO>(); } }
		public Table<A015TipOLogRadOurO> A015TipOLogRadOurO { get { return GetTable<A015TipOLogRadOurO>(); } }
		public Table<A016FormAAtUAcAO> A016FormAAtUAcAO { get { return GetTable<A016FormAAtUAcAO>(); } }
		public Table<A017PaIs> A017PaIs { get { return GetTable<A017PaIs>(); } }
		public Table<A018TipOPeSsOAJuridicA> A018TipOPeSsOAJuridicA { get { return GetTable<A018TipOPeSsOAJuridicA>(); } }
		public Table<A019QuaLiFicaCaOAssOCiaCaO> A019QuaLiFicaCaOAssOCiaCaO { get { return GetTable<A019QuaLiFicaCaOAssOCiaCaO>(); } }
		public Table<A020ProfIsSao> A020ProfIsSao { get { return GetTable<A020ProfIsSao>(); } }
		public Table<A021MotIVOBaIXA> A021MotIVOBaIXA { get { return GetTable<A021MotIVOBaIXA>(); } }
		public Table<R001VInCuLo> R001VInCuLo { get { return GetTable<R001VInCuLo>(); } }
		public Table<R002VInCuLoEnderEcO> R002VInCuLoEnderEcO { get { return GetTable<R002VInCuLoEnderEcO>(); } }
		public Table<R003ReLOrGMuNiC> R003ReLOrGMuNiC { get { return GetTable<R003ReLOrGMuNiC>(); } }
		public Table<R004AtUAcAO> R004AtUAcAO { get { return GetTable<R004AtUAcAO>(); } }
		public Table<R005ProtocolOEventO> R005ProtocolOEventO { get { return GetTable<R005ProtocolOEventO>(); } }
		public Table<T001PeSsOA> T001PeSsOA { get { return GetTable<T001PeSsOA>(); } }
		public Table<T002PeSsOAFIsiCa> T002PeSsOAFIsiCa { get { return GetTable<T002PeSsOAFIsiCa>(); } }
		public Table<T003PeSsOAJuridicA> T003PeSsOAJuridicA { get { return GetTable<T003PeSsOAJuridicA>(); } }
		public Table<T004OrGaoRegisTRO> T004OrGaoRegisTRO { get { return GetTable<T004OrGaoRegisTRO>(); } }
		public Table<T005ProtocolO> T005ProtocolO { get { return GetTable<T005ProtocolO>(); } }
		public Table<T006ProtocolOReQUERimeNto> T006ProtocolOReQUERimeNto { get { return GetTable<T006ProtocolOReQUERimeNto>(); } }
		public Table<TabActVDesC> TabActVDesC { get { return GetTable<TabActVDesC>(); } }
		public Table<TabActVEcOn> TabActVEcOn { get { return GetTable<TabActVEcOn>(); } }
		public Table<TabCePTipO> TabCePTipO { get { return GetTable<TabCePTipO>(); } }
		public Table<TabCePUF> TabCePUF { get { return GetTable<TabCePUF>(); } }
		public Table<TabGenericA> TabGenericA { get { return GetTable<TabGenericA>(); } }
		public Table<TabMuNiC> TabMuNiC { get { return GetTable<TabMuNiC>(); } }

	}

	[Table(Name = "requerimento.a001_atividade")]
	public partial class A001AtIViDade : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string A001CoatIViDade

		private string _a001cOatIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_a001cOatIvIDade", Name = "A001_CO_ATIVIDADE", DbType = "varchar(7)", CanBeNull = false)]
		public string A001CoatIViDade
		{
			get
			{
				return _a001cOatIvIDade;
			}
			set
			{
				if (value != _a001cOatIvIDade)
				{
					_a001cOatIvIDade = value;
					OnPropertyChanged("A001CoatIViDade");
				}
			}
		}

		#endregion

		#region string A001DSatIViDade

		private string _a001dsAtIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_a001dsAtIvIDade", Name = "A001_DS_ATIVIDADE", DbType = "varchar(500)")]
		public string A001DSatIViDade
		{
			get
			{
				return _a001dsAtIvIDade;
			}
			set
			{
				if (value != _a001dsAtIvIDade)
				{
					_a001dsAtIvIDade = value;
					OnPropertyChanged("A001DSatIViDade");
				}
			}
		}

		#endregion

		#region string A001InTipO

		private string _a001iNTipO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a001iNTipO", Name = "A001_IN_TIPO", DbType = "varchar(1)", CanBeNull = false)]
		public string A001InTipO
		{
			get
			{
				return _a001iNTipO;
			}
			set
			{
				if (value != _a001iNTipO)
				{
					_a001iNTipO = value;
					OnPropertyChanged("A001InTipO");
				}
			}
		}

		#endregion

		#region ctor

		public A001AtIViDade()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a002_ato")]
	public partial class A002AtO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A002CoatO

		private int _a002cOatO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a002cOatO", Name = "A002_CO_ATO", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int A002CoatO
		{
			get
			{
				return _a002cOatO;
			}
			set
			{
				if (value != _a002cOatO)
				{
					_a002cOatO = value;
					OnPropertyChanged("A002CoatO");
				}
			}
		}

		#endregion

		#region string A002DSatO

		private string _a002dsAtO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a002dsAtO", Name = "A002_DS_ATO", DbType = "varchar(60)")]
		public string A002DSatO
		{
			get
			{
				return _a002dsAtO;
			}
			set
			{
				if (value != _a002dsAtO)
				{
					_a002dsAtO = value;
					OnPropertyChanged("A002DSatO");
				}
			}
		}

		#endregion

		#region string A002NRatO

		private string _a002nrAtO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a002nrAtO", Name = "A002_NR_ATO", DbType = "varchar(20)")]
		public string A002NRatO
		{
			get
			{
				return _a002nrAtO;
			}
			set
			{
				if (value != _a002nrAtO)
				{
					_a002nrAtO = value;
					OnPropertyChanged("A002NRatO");
				}
			}
		}

		#endregion

		#region ctor

		public A002AtO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a003_evento")]
	public partial class A003EventO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A002CoatO

		private int _a002cOatO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a002cOatO", Name = "A002_CO_ATO", DbType = "int", CanBeNull = false)]
		public int A002CoatO
		{
			get
			{
				return _a002cOatO;
			}
			set
			{
				if (value != _a002cOatO)
				{
					_a002cOatO = value;
					OnPropertyChanged("A002CoatO");
				}
			}
		}

		#endregion

		#region int A003COeVentO

		private int _a003coEVentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a003coEVentO", Name = "A003_CO_EVENTO", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int A003COeVentO
		{
			get
			{
				return _a003coEVentO;
			}
			set
			{
				if (value != _a003coEVentO)
				{
					_a003coEVentO = value;
					OnPropertyChanged("A003COeVentO");
				}
			}
		}

		#endregion

		#region string A003DSevenTo

		private string _a003dsEvenTo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a003dsEvenTo", Name = "A003_DS_EVENTO", DbType = "varchar(500)")]
		public string A003DSevenTo
		{
			get
			{
				return _a003dsEvenTo;
			}
			set
			{
				if (value != _a003dsEvenTo)
				{
					_a003dsEvenTo = value;
					OnPropertyChanged("A003DSevenTo");
				}
			}
		}

		#endregion

		#region string A003NReVentO

		private string _a003nrEVentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a003nrEVentO", Name = "A003_NR_EVENTO", DbType = "varchar(500)")]
		public string A003NReVentO
		{
			get
			{
				return _a003nrEVentO;
			}
			set
			{
				if (value != _a003nrEVentO)
				{
					_a003nrEVentO = value;
					OnPropertyChanged("A003NReVentO");
				}
			}
		}

		#endregion

		#region ctor

		public A003EventO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a004_uf")]
	public partial class A004UF : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string A004COUF

		private string _a004couf;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004couf", Name = "A004_CO_UF", DbType = "varchar(2)", CanBeNull = false)]
		public string A004COUF
		{
			get
			{
				return _a004couf;
			}
			set
			{
				if (value != _a004couf)
				{
					_a004couf = value;
					OnPropertyChanged("A004COUF");
				}
			}
		}

		#endregion

		#region string A004DSUF

		private string _a004dsuf;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004dsuf", Name = "A004_DS_UF", DbType = "varchar(30)")]
		public string A004DSUF
		{
			get
			{
				return _a004dsuf;
			}
			set
			{
				if (value != _a004dsuf)
				{
					_a004dsuf = value;
					OnPropertyChanged("A004DSUF");
				}
			}
		}

		#endregion

		#region ctor

		public A004UF()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a005_municipio")]
	public partial class A005MuNiCipIo : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region decimal A005ComUnICipIo

		private decimal _a005cOmUnIcIpIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a005cOmUnIcIpIo", Name = "A005_CO_MUNICIPIO", DbType = "decimal(6,0)", CanBeNull = false)]
		public decimal A005ComUnICipIo
		{
			get
			{
				return _a005cOmUnIcIpIo;
			}
			set
			{
				if (value != _a005cOmUnIcIpIo)
				{
					_a005cOmUnIcIpIo = value;
					OnPropertyChanged("A005ComUnICipIo");
				}
			}
		}

		#endregion

		#region string A005DSmUnICipIo

		private string _a005dsMUnIcIpIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a005dsMUnIcIpIo", Name = "A005_DS_MUNICIPIO", DbType = "varchar(60)")]
		public string A005DSmUnICipIo
		{
			get
			{
				return _a005dsMUnIcIpIo;
			}
			set
			{
				if (value != _a005dsMUnIcIpIo)
				{
					_a005dsMUnIcIpIo = value;
					OnPropertyChanged("A005DSmUnICipIo");
				}
			}
		}

		#endregion

		#region ctor

		public A005MuNiCipIo()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a006_natureza_juridica")]
	public partial class A006NatureZAJuridicA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A006CONatureZAJuridicA

		private int _a006conAtureZajUridicA;
		[DebuggerNonUserCode]
		[Column(Storage = "_a006conAtureZajUridicA", Name = "A006_CO_NATUREZA_JURIDICA", DbType = "int", CanBeNull = false)]
		public int A006CONatureZAJuridicA
		{
			get
			{
				return _a006conAtureZajUridicA;
			}
			set
			{
				if (value != _a006conAtureZajUridicA)
				{
					_a006conAtureZajUridicA = value;
					OnPropertyChanged("A006CONatureZAJuridicA");
				}
			}
		}

		#endregion

		#region string A006DSNatureZAJuridicA

		private string _a006dsnAtureZajUridicA;
		[DebuggerNonUserCode]
		[Column(Storage = "_a006dsnAtureZajUridicA", Name = "A006_DS_NATUREZA_JURIDICA", DbType = "varchar(120)", CanBeNull = false)]
		public string A006DSNatureZAJuridicA
		{
			get
			{
				return _a006dsnAtureZajUridicA;
			}
			set
			{
				if (value != _a006dsnAtureZajUridicA)
				{
					_a006dsnAtureZajUridicA = value;
					OnPropertyChanged("A006DSNatureZAJuridicA");
				}
			}
		}

		#endregion

		#region ctor

		public A006NatureZAJuridicA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a007_situacao")]
	public partial class A007SituAcAO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A007CosItuAcAO

		private int _a007cOsItuAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a007cOsItuAcAo", Name = "A007_CO_SITUACAO", DbType = "int", CanBeNull = false)]
		public int A007CosItuAcAO
		{
			get
			{
				return _a007cOsItuAcAo;
			}
			set
			{
				if (value != _a007cOsItuAcAo)
				{
					_a007cOsItuAcAo = value;
					OnPropertyChanged("A007CosItuAcAO");
				}
			}
		}

		#endregion

		#region string A007DSsItuAcAO

		private string _a007dsSItuAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a007dsSItuAcAo", Name = "A007_DS_SITUACAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A007DSsItuAcAO
		{
			get
			{
				return _a007dsSItuAcAo;
			}
			set
			{
				if (value != _a007dsSItuAcAo)
				{
					_a007dsSItuAcAo = value;
					OnPropertyChanged("A007DSsItuAcAO");
				}
			}
		}

		#endregion

		#region ctor

		public A007SituAcAO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a008_status_atual")]
	public partial class A008StatusAtUAL : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A008CostaTUsAtUAL

		private int _a008cOstaTuSAtUal;
		[DebuggerNonUserCode]
		[Column(Storage = "_a008cOstaTuSAtUal", Name = "A008_CO_STATUS_ATUAL", DbType = "int", CanBeNull = false)]
		public int A008CostaTUsAtUAL
		{
			get
			{
				return _a008cOstaTuSAtUal;
			}
			set
			{
				if (value != _a008cOstaTuSAtUal)
				{
					_a008cOstaTuSAtUal = value;
					OnPropertyChanged("A008CostaTUsAtUAL");
				}
			}
		}

		#endregion

		#region string A008DSStatusAtUAL

		private string _a008dssTatusAtUal;
		[DebuggerNonUserCode]
		[Column(Storage = "_a008dssTatusAtUal", Name = "A008_DS_STATUS_ATUAL", DbType = "varchar(120)", CanBeNull = false)]
		public string A008DSStatusAtUAL
		{
			get
			{
				return _a008dssTatusAtUal;
			}
			set
			{
				if (value != _a008dssTatusAtUal)
				{
					_a008dssTatusAtUal = value;
					OnPropertyChanged("A008DSStatusAtUAL");
				}
			}
		}

		#endregion

		#region ctor

		public A008StatusAtUAL()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a009_condicao")]
	public partial class A009ConDiCaO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A009CocoNDiCaO

		private int _a009cOcoNdICaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a009cOcoNdICaO", Name = "A009_CO_CONDICAO", DbType = "int", CanBeNull = false)]
		public int A009CocoNDiCaO
		{
			get
			{
				return _a009cOcoNdICaO;
			}
			set
			{
				if (value != _a009cOcoNdICaO)
				{
					_a009cOcoNdICaO = value;
					OnPropertyChanged("A009CocoNDiCaO");
				}
			}
		}

		#endregion

		#region string A009DSConDiCaO

		private string _a009dscOnDiCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a009dscOnDiCaO", Name = "A009_DS_CONDICAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A009DSConDiCaO
		{
			get
			{
				return _a009dscOnDiCaO;
			}
			set
			{
				if (value != _a009dscOnDiCaO)
				{
					_a009dscOnDiCaO = value;
					OnPropertyChanged("A009DSConDiCaO");
				}
			}
		}

		#endregion

		#region ctor

		public A009ConDiCaO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a010_tipo_documento")]
	public partial class A010TipODocumentO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A010CotIPODocumentO

		private int _a010cOtIpodOcumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a010cOtIpodOcumentO", Name = "A010_CO_TIPO_DOCUMENTO", DbType = "int", CanBeNull = false)]
		public int A010CotIPODocumentO
		{
			get
			{
				return _a010cOtIpodOcumentO;
			}
			set
			{
				if (value != _a010cOtIpodOcumentO)
				{
					_a010cOtIpodOcumentO = value;
					OnPropertyChanged("A010CotIPODocumentO");
				}
			}
		}

		#endregion

		#region string A010DstIPODocumentO

		private string _a010dStIpodOcumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a010dStIpodOcumentO", Name = "A010_DS_TIPO_DOCUMENTO", DbType = "varchar(120)", CanBeNull = false)]
		public string A010DstIPODocumentO
		{
			get
			{
				return _a010dStIpodOcumentO;
			}
			set
			{
				if (value != _a010dStIpodOcumentO)
				{
					_a010dStIpodOcumentO = value;
					OnPropertyChanged("A010DstIPODocumentO");
				}
			}
		}

		#endregion

		#region ctor

		public A010TipODocumentO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a011_porte")]
	public partial class A011Porte : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A011CopOrTe

		private int _a011cOpOrTe;
		[DebuggerNonUserCode]
		[Column(Storage = "_a011cOpOrTe", Name = "A011_CO_PORTE", DbType = "int", CanBeNull = false)]
		public int A011CopOrTe
		{
			get
			{
				return _a011cOpOrTe;
			}
			set
			{
				if (value != _a011cOpOrTe)
				{
					_a011cOpOrTe = value;
					OnPropertyChanged("A011CopOrTe");
				}
			}
		}

		#endregion

		#region string A011DSportE

		private string _a011dsPortE;
		[DebuggerNonUserCode]
		[Column(Storage = "_a011dsPortE", Name = "A011_DS_PORTE", DbType = "varchar(120)", CanBeNull = false)]
		public string A011DSportE
		{
			get
			{
				return _a011dsPortE;
			}
			set
			{
				if (value != _a011dsPortE)
				{
					_a011dsPortE = value;
					OnPropertyChanged("A011DSportE");
				}
			}
		}

		#endregion

		#region ctor

		public A011Porte()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a012_estado_civil")]
	public partial class A012EstAdoCivil : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A012COEstAdoCivil

		private int _a012coeStAdoCivil;
		[DebuggerNonUserCode]
		[Column(Storage = "_a012coeStAdoCivil", Name = "A012_CO_ESTADO_CIVIL", DbType = "int", CanBeNull = false)]
		public int A012COEstAdoCivil
		{
			get
			{
				return _a012coeStAdoCivil;
			}
			set
			{
				if (value != _a012coeStAdoCivil)
				{
					_a012coeStAdoCivil = value;
					OnPropertyChanged("A012COEstAdoCivil");
				}
			}
		}

		#endregion

		#region string A012DSEstAdoCivil

		private string _a012dseStAdoCivil;
		[DebuggerNonUserCode]
		[Column(Storage = "_a012dseStAdoCivil", Name = "A012_DS_ESTADO_CIVIL", DbType = "varchar(120)", CanBeNull = false)]
		public string A012DSEstAdoCivil
		{
			get
			{
				return _a012dseStAdoCivil;
			}
			set
			{
				if (value != _a012dseStAdoCivil)
				{
					_a012dseStAdoCivil = value;
					OnPropertyChanged("A012DSEstAdoCivil");
				}
			}
		}

		#endregion

		#region ctor

		public A012EstAdoCivil()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a013_regime_bens")]
	public partial class A013RegimeBenS : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A013CoreGiMeBenS

		private int _a013cOreGiMeBenS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a013cOreGiMeBenS", Name = "A013_CO_REGIME_BENS", DbType = "int", CanBeNull = false)]
		public int A013CoreGiMeBenS
		{
			get
			{
				return _a013cOreGiMeBenS;
			}
			set
			{
				if (value != _a013cOreGiMeBenS)
				{
					_a013cOreGiMeBenS = value;
					OnPropertyChanged("A013CoreGiMeBenS");
				}
			}
		}

		#endregion

		#region string A013DSRegimeBenS

		private string _a013dsrEgimeBenS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a013dsrEgimeBenS", Name = "A013_DS_REGIME_BENS", DbType = "varchar(120)", CanBeNull = false)]
		public string A013DSRegimeBenS
		{
			get
			{
				return _a013dsrEgimeBenS;
			}
			set
			{
				if (value != _a013dsrEgimeBenS)
				{
					_a013dsrEgimeBenS = value;
					OnPropertyChanged("A013DSRegimeBenS");
				}
			}
		}

		#endregion

		#region ctor

		public A013RegimeBenS()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a014_emancipacao")]
	public partial class A014EmAnCipAcAO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A014COeManCipAcAO

		private int _a014coEManCipAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a014coEManCipAcAo", Name = "A014_CO_EMANCIPACAO", DbType = "int", CanBeNull = false)]
		public int A014COeManCipAcAO
		{
			get
			{
				return _a014coEManCipAcAo;
			}
			set
			{
				if (value != _a014coEManCipAcAo)
				{
					_a014coEManCipAcAo = value;
					OnPropertyChanged("A014COeManCipAcAO");
				}
			}
		}

		#endregion

		#region string A014DSEmAnCipAcAO

		private string _a014dseMAnCipAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a014dseMAnCipAcAo", Name = "A014_DS_EMANCIPACAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A014DSEmAnCipAcAO
		{
			get
			{
				return _a014dseMAnCipAcAo;
			}
			set
			{
				if (value != _a014dseMAnCipAcAo)
				{
					_a014dseMAnCipAcAo = value;
					OnPropertyChanged("A014DSEmAnCipAcAO");
				}
			}
		}

		#endregion

		#region ctor

		public A014EmAnCipAcAO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a015_tipo_logradouro")]
	public partial class A015TipOLogRadOurO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region decimal A015CotIPoloGradOurO

		private decimal _a015cOtIpOloGradOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a015cOtIpOloGradOurO", Name = "A015_CO_TIPO_LOGRADOURO", DbType = "decimal", CanBeNull = false)]
		public decimal A015CotIPoloGradOurO
		{
			get
			{
				return _a015cOtIpOloGradOurO;
			}
			set
			{
				if (value != _a015cOtIpOloGradOurO)
				{
					_a015cOtIpOloGradOurO = value;
					OnPropertyChanged("A015CotIPoloGradOurO");
				}
			}
		}

		#endregion

		#region string A015DstIPoloGradOurO

		private string _a015dStIpOloGradOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a015dStIpOloGradOurO", Name = "A015_DS_TIPO_LOGRADOURO", DbType = "varchar(50)", CanBeNull = false)]
		public string A015DstIPoloGradOurO
		{
			get
			{
				return _a015dStIpOloGradOurO;
			}
			set
			{
				if (value != _a015dStIpOloGradOurO)
				{
					_a015dStIpOloGradOurO = value;
					OnPropertyChanged("A015DstIPoloGradOurO");
				}
			}
		}

		#endregion

		#region ctor

		public A015TipOLogRadOurO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a016_forma_atuacao")]
	public partial class A016FormAAtUAcAO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A016COFormAAtUAcAO

		private int _a016cofOrmAaTUaCAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a016cofOrmAaTUaCAo", Name = "A016_CO_FORMA_ATUACAO", DbType = "int", CanBeNull = false)]
		public int A016COFormAAtUAcAO
		{
			get
			{
				return _a016cofOrmAaTUaCAo;
			}
			set
			{
				if (value != _a016cofOrmAaTUaCAo)
				{
					_a016cofOrmAaTUaCAo = value;
					OnPropertyChanged("A016COFormAAtUAcAO");
				}
			}
		}

		#endregion

		#region string A016DSFormAAtUAcAO

		private string _a016dsfOrmAaTUaCAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a016dsfOrmAaTUaCAo", Name = "A016_DS_FORMA_ATUACAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A016DSFormAAtUAcAO
		{
			get
			{
				return _a016dsfOrmAaTUaCAo;
			}
			set
			{
				if (value != _a016dsfOrmAaTUaCAo)
				{
					_a016dsfOrmAaTUaCAo = value;
					OnPropertyChanged("A016DSFormAAtUAcAO");
				}
			}
		}

		#endregion

		#region ctor

		public A016FormAAtUAcAO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a017_pais")]
	public partial class A017PaIs : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A017CopAiS

		private int _a017cOpAiS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a017cOpAiS", Name = "A017_CO_PAIS", DbType = "int", CanBeNull = false)]
		public int A017CopAiS
		{
			get
			{
				return _a017cOpAiS;
			}
			set
			{
				if (value != _a017cOpAiS)
				{
					_a017cOpAiS = value;
					OnPropertyChanged("A017CopAiS");
				}
			}
		}

		#endregion

		#region string A017DSpaIs

		private string _a017dsPaIs;
		[DebuggerNonUserCode]
		[Column(Storage = "_a017dsPaIs", Name = "A017_DS_PAIS", DbType = "varchar(120)", CanBeNull = false)]
		public string A017DSpaIs
		{
			get
			{
				return _a017dsPaIs;
			}
			set
			{
				if (value != _a017dsPaIs)
				{
					_a017dsPaIs = value;
					OnPropertyChanged("A017DSpaIs");
				}
			}
		}

		#endregion

		#region ctor

		public A017PaIs()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a018_tipo_pessoa_juridica")]
	public partial class A018TipOPeSsOAJuridicA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A018CotIPopeSsOAJuridicA

		private int _a018cOtIpOpeSsOajUridicA;
		[DebuggerNonUserCode]
		[Column(Storage = "_a018cOtIpOpeSsOajUridicA", Name = "A018_CO_TIPO_PESSOA_JURIDICA", DbType = "int", CanBeNull = false)]
		public int A018CotIPopeSsOAJuridicA
		{
			get
			{
				return _a018cOtIpOpeSsOajUridicA;
			}
			set
			{
				if (value != _a018cOtIpOpeSsOajUridicA)
				{
					_a018cOtIpOpeSsOajUridicA = value;
					OnPropertyChanged("A018CotIPopeSsOAJuridicA");
				}
			}
		}

		#endregion

		#region string A018DstIPopeSsOAJuridicA

		private string _a018dStIpOpeSsOajUridicA;
		[DebuggerNonUserCode]
		[Column(Storage = "_a018dStIpOpeSsOajUridicA", Name = "A018_DS_TIPO_PESSOA_JURIDICA", DbType = "varchar(120)", CanBeNull = false)]
		public string A018DstIPopeSsOAJuridicA
		{
			get
			{
				return _a018dStIpOpeSsOajUridicA;
			}
			set
			{
				if (value != _a018dStIpOpeSsOajUridicA)
				{
					_a018dStIpOpeSsOajUridicA = value;
					OnPropertyChanged("A018DstIPopeSsOAJuridicA");
				}
			}
		}

		#endregion

		#region ctor

		public A018TipOPeSsOAJuridicA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a019_qualificacao_associacao")]
	public partial class A019QuaLiFicaCaOAssOCiaCaO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A019COQuaLiFicaCaOasSoCiaCaO

		private int _a019coqUaLiFicaCaOasSoCiaCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a019coqUaLiFicaCaOasSoCiaCaO", Name = "A019_CO_QUALIFICACAO_ASSOCIACAO", DbType = "int", CanBeNull = false)]
		public int A019COQuaLiFicaCaOasSoCiaCaO
		{
			get
			{
				return _a019coqUaLiFicaCaOasSoCiaCaO;
			}
			set
			{
				if (value != _a019coqUaLiFicaCaOasSoCiaCaO)
				{
					_a019coqUaLiFicaCaOasSoCiaCaO = value;
					OnPropertyChanged("A019COQuaLiFicaCaOasSoCiaCaO");
				}
			}
		}

		#endregion

		#region string A019DSQuaLiFicaCaOasSoCiaCaO

		private string _a019dsqUaLiFicaCaOasSoCiaCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a019dsqUaLiFicaCaOasSoCiaCaO", Name = "A019_DS_QUALIFICACAO_ASSOCIACAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A019DSQuaLiFicaCaOasSoCiaCaO
		{
			get
			{
				return _a019dsqUaLiFicaCaOasSoCiaCaO;
			}
			set
			{
				if (value != _a019dsqUaLiFicaCaOasSoCiaCaO)
				{
					_a019dsqUaLiFicaCaOasSoCiaCaO = value;
					OnPropertyChanged("A019DSQuaLiFicaCaOasSoCiaCaO");
				}
			}
		}

		#endregion

		#region ctor

		public A019QuaLiFicaCaOAssOCiaCaO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a020_profissao")]
	public partial class A020ProfIsSao : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A020CopROfIsSao

		private int _a020cOpRoFIsSao;
		[DebuggerNonUserCode]
		[Column(Storage = "_a020cOpRoFIsSao", Name = "A020_CO_PROFISSAO", DbType = "int", CanBeNull = false)]
		public int A020CopROfIsSao
		{
			get
			{
				return _a020cOpRoFIsSao;
			}
			set
			{
				if (value != _a020cOpRoFIsSao)
				{
					_a020cOpRoFIsSao = value;
					OnPropertyChanged("A020CopROfIsSao");
				}
			}
		}

		#endregion

		#region string A020DSProfIsSao

		private string _a020dspRofIsSao;
		[DebuggerNonUserCode]
		[Column(Storage = "_a020dspRofIsSao", Name = "A020_DS_PROFISSAO", DbType = "varchar(120)", CanBeNull = false)]
		public string A020DSProfIsSao
		{
			get
			{
				return _a020dspRofIsSao;
			}
			set
			{
				if (value != _a020dspRofIsSao)
				{
					_a020dspRofIsSao = value;
					OnPropertyChanged("A020DSProfIsSao");
				}
			}
		}

		#endregion

		#region ctor

		public A020ProfIsSao()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.a021_motivo_baixa")]
	public partial class A021MotIVOBaIXA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A020ComOtIVOBaIXA

		private int _a020cOmOtIvobAIxa;
		[DebuggerNonUserCode]
		[Column(Storage = "_a020cOmOtIvobAIxa", Name = "A020_CO_MOTIVO_BAIXA", DbType = "int", CanBeNull = false)]
		public int A020ComOtIVOBaIXA
		{
			get
			{
				return _a020cOmOtIvobAIxa;
			}
			set
			{
				if (value != _a020cOmOtIvobAIxa)
				{
					_a020cOmOtIvobAIxa = value;
					OnPropertyChanged("A020ComOtIVOBaIXA");
				}
			}
		}

		#endregion

		#region string A020DSmOtIVOBaIXA

		private string _a020dsMOtIvobAIxa;
		[DebuggerNonUserCode]
		[Column(Storage = "_a020dsMOtIvobAIxa", Name = "A020_DS_MOTIVO_BAIXA", DbType = "varchar(120)", CanBeNull = false)]
		public string A020DSmOtIVOBaIXA
		{
			get
			{
				return _a020dsMOtIvobAIxa;
			}
			set
			{
				if (value != _a020dsMOtIvobAIxa)
				{
					_a020dsMOtIvobAIxa = value;
					OnPropertyChanged("A020DSmOtIVOBaIXA");
				}
			}
		}

		#endregion

		#region ctor

		public A021MotIVOBaIXA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.r001_vinculo")]
	public partial class R001VInCuLo : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A009CocoNDiCaO

		private int _a009cOcoNdICaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a009cOcoNdICaO", Name = "A009_CO_CONDICAO", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int A009CocoNDiCaO
		{
			get
			{
				return _a009cOcoNdICaO;
			}
			set
			{
				if (value != _a009cOcoNdICaO)
				{
					_a009cOcoNdICaO = value;
					OnPropertyChanged("A009CocoNDiCaO");
				}
			}
		}

		#endregion

		#region string R001DScarGodIreCaO

		private string _r001dsCarGodIreCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001dsCarGodIreCaO", Name = "R001_DS_CARGO_DIRECAO", DbType = "varchar(50)")]
		public string R001DScarGodIreCaO
		{
			get
			{
				return _r001dsCarGodIreCaO;
			}
			set
			{
				if (value != _r001dsCarGodIreCaO)
				{
					_r001dsCarGodIreCaO = value;
					OnPropertyChanged("R001DScarGodIreCaO");
				}
			}
		}

		#endregion

		#region DateTime R001DTentRadAvInCuLo

		private DateTime _r001dtEntRadAvInCuLo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001dtEntRadAvInCuLo", Name = "R001_DT_ENTRADA_VINCULO", DbType = "date", IsPrimaryKey = true, CanBeNull = false)]
		public DateTime R001DTentRadAvInCuLo
		{
			get
			{
				return _r001dtEntRadAvInCuLo;
			}
			set
			{
				if (value != _r001dtEntRadAvInCuLo)
				{
					_r001dtEntRadAvInCuLo = value;
					OnPropertyChanged("R001DTentRadAvInCuLo");
				}
			}
		}

		#endregion

		#region DateTime? R001DTInICIoManDatO

		private DateTime? _r001dtiNIciOManDatO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001dtiNIciOManDatO", Name = "R001_DT_INICIO_MANDATO", DbType = "date")]
		public DateTime? R001DTInICIoManDatO
		{
			get
			{
				return _r001dtiNIciOManDatO;
			}
			set
			{
				if (value != _r001dtiNIciOManDatO)
				{
					_r001dtiNIciOManDatO = value;
					OnPropertyChanged("R001DTInICIoManDatO");
				}
			}
		}

		#endregion

		#region DateTime? R001DTSaidAvInCuLo

		private DateTime? _r001dtsAidAvInCuLo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001dtsAidAvInCuLo", Name = "R001_DT_SAIDA_VINCULO", DbType = "date")]
		public DateTime? R001DTSaidAvInCuLo
		{
			get
			{
				return _r001dtsAidAvInCuLo;
			}
			set
			{
				if (value != _r001dtsAidAvInCuLo)
				{
					_r001dtsAidAvInCuLo = value;
					OnPropertyChanged("R001DTSaidAvInCuLo");
				}
			}
		}

		#endregion

		#region DateTime? R001DTTermInOmanDatO

		private DateTime? _r001dttErmInOmanDatO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001dttErmInOmanDatO", Name = "R001_DT_TERMINO_MANDATO", DbType = "date")]
		public DateTime? R001DTTermInOmanDatO
		{
			get
			{
				return _r001dttErmInOmanDatO;
			}
			set
			{
				if (value != _r001dttErmInOmanDatO)
				{
					_r001dttErmInOmanDatO = value;
					OnPropertyChanged("R001DTTermInOmanDatO");
				}
			}
		}

		#endregion

		#region string R001InGeRentEUsoFirmA

		private string _r001iNGeRentEuSoFirmA;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001iNGeRentEuSoFirmA", Name = "R001_IN_GERENTE_USO_FIRMA", DbType = "char(1)")]
		public string R001InGeRentEUsoFirmA
		{
			get
			{
				return _r001iNGeRentEuSoFirmA;
			}
			set
			{
				if (value != _r001iNGeRentEuSoFirmA)
				{
					_r001iNGeRentEuSoFirmA = value;
					OnPropertyChanged("R001InGeRentEUsoFirmA");
				}
			}
		}

		#endregion

		#region string R001InSituAcAO

		private string _r001iNSituAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001iNSituAcAo", Name = "R001_IN_SITUACAO", DbType = "char(1)")]
		public string R001InSituAcAO
		{
			get
			{
				return _r001iNSituAcAo;
			}
			set
			{
				if (value != _r001iNSituAcAo)
				{
					_r001iNSituAcAo = value;
					OnPropertyChanged("R001InSituAcAO");
				}
			}
		}

		#endregion

		#region decimal? R001VlpArtICipAcAO

		private decimal? _r001vLpArtIcIpAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r001vLpArtIcIpAcAo", Name = "R001_VL_PARTICIPACAO", DbType = "decimal")]
		public decimal? R001VlpArtICipAcAO
		{
			get
			{
				return _r001vLpArtIcIpAcAo;
			}
			set
			{
				if (value != _r001vLpArtIcIpAcAo)
				{
					_r001vLpArtIcIpAcAo = value;
					OnPropertyChanged("R001VlpArtICipAcAO");
				}
			}
		}

		#endregion

		#region int T001SQPeSsOA

		private int _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region int T001SQPeSSoapAi

		private int _t001sqpESsOapAi;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOapAi", Name = "T001_SQ_PESSOA_PAI", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int T001SQPeSSoapAi
		{
			get
			{
				return _t001sqpESsOapAi;
			}
			set
			{
				if (value != _t001sqpESsOapAi)
				{
					_t001sqpESsOapAi = value;
					OnPropertyChanged("T001SQPeSSoapAi");
				}
			}
		}

		#endregion

		#region int? T001SQPeSsOarEPLegal

		private int? _t001sqpESsOarEplEgal;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOarEplEgal", Name = "T001_SQ_PESSOA_REP_LEGAL", DbType = "int")]
		public int? T001SQPeSsOarEPLegal
		{
			get
			{
				return _t001sqpESsOarEplEgal;
			}
			set
			{
				if (value != _t001sqpESsOarEplEgal)
				{
					_t001sqpESsOarEplEgal = value;
					OnPropertyChanged("T001SQPeSsOarEPLegal");
				}
			}
		}

		#endregion

		#region ctor

		public R001VInCuLo()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.r002_vinculo_endereco")]
	public partial class R002VInCuLoEnderEcO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int? A004CopAiS

		private int? _a004cOpAiS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004cOpAiS", Name = "A004_CO_PAIS", DbType = "int")]
		public int? A004CopAiS
		{
			get
			{
				return _a004cOpAiS;
			}
			set
			{
				if (value != _a004cOpAiS)
				{
					_a004cOpAiS = value;
					OnPropertyChanged("A004CopAiS");
				}
			}
		}

		#endregion

		#region int? A005ComUnICipIo

		private int? _a005cOmUnIcIpIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a005cOmUnIcIpIo", Name = "A005_CO_MUNICIPIO", DbType = "int")]
		public int? A005ComUnICipIo
		{
			get
			{
				return _a005cOmUnIcIpIo;
			}
			set
			{
				if (value != _a005cOmUnIcIpIo)
				{
					_a005cOmUnIcIpIo = value;
					OnPropertyChanged("A005ComUnICipIo");
				}
			}
		}

		#endregion

		#region int? A015CotIPoloGradOurO

		private int? _a015cOtIpOloGradOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a015cOtIpOloGradOurO", Name = "A015_CO_TIPO_LOGRADOURO", DbType = "int")]
		public int? A015CotIPoloGradOurO
		{
			get
			{
				return _a015cOtIpOloGradOurO;
			}
			set
			{
				if (value != _a015cOtIpOloGradOurO)
				{
					_a015cOtIpOloGradOurO = value;
					OnPropertyChanged("A015CotIPoloGradOurO");
				}
			}
		}

		#endregion

		#region string R002DSbaIrRO

		private string _r002dsBaIrRo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002dsBaIrRo", Name = "R002_DS_BAIRRO", DbType = "varchar(30)")]
		public string R002DSbaIrRO
		{
			get
			{
				return _r002dsBaIrRo;
			}
			set
			{
				if (value != _r002dsBaIrRo)
				{
					_r002dsBaIrRo = value;
					OnPropertyChanged("R002DSbaIrRO");
				}
			}
		}

		#endregion

		#region string R002DSComplementO

		private string _r002dscOmplementO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002dscOmplementO", Name = "R002_DS_COMPLEMENTO", DbType = "varchar(15)")]
		public string R002DSComplementO
		{
			get
			{
				return _r002dscOmplementO;
			}
			set
			{
				if (value != _r002dscOmplementO)
				{
					_r002dscOmplementO = value;
					OnPropertyChanged("R002DSComplementO");
				}
			}
		}

		#endregion

		#region string R002DSlogRadOurO

		private string _r002dsLogRadOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002dsLogRadOurO", Name = "R002_DS_LOGRADOURO", DbType = "varchar(60)")]
		public string R002DSlogRadOurO
		{
			get
			{
				return _r002dsLogRadOurO;
			}
			set
			{
				if (value != _r002dsLogRadOurO)
				{
					_r002dsLogRadOurO = value;
					OnPropertyChanged("R002DSlogRadOurO");
				}
			}
		}

		#endregion

		#region string R002DSReferEnCia

		private string _r002dsrEferEnCia;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002dsrEferEnCia", Name = "R002_DS_REFERENCIA", DbType = "varchar(50)")]
		public string R002DSReferEnCia
		{
			get
			{
				return _r002dsrEferEnCia;
			}
			set
			{
				if (value != _r002dsrEferEnCia)
				{
					_r002dsrEferEnCia = value;
					OnPropertyChanged("R002DSReferEnCia");
				}
			}
		}

		#endregion

		#region int R002IDSEQvInCEnd

		private int _r002idseqVInCeNd;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002idseqVInCeNd", Name = "R002_ID_SEQ_VINC_END", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int R002IDSEQvInCEnd
		{
			get
			{
				return _r002idseqVInCeNd;
			}
			set
			{
				if (value != _r002idseqVInCeNd)
				{
					_r002idseqVInCeNd = value;
					OnPropertyChanged("R002IDSEQvInCEnd");
				}
			}
		}

		#endregion

		#region string R002NRcEP

		private string _r002nrCEp;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002nrCEp", Name = "R002_NR_CEP", DbType = "varchar(8)")]
		public string R002NRcEP
		{
			get
			{
				return _r002nrCEp;
			}
			set
			{
				if (value != _r002nrCEp)
				{
					_r002nrCEp = value;
					OnPropertyChanged("R002NRcEP");
				}
			}
		}

		#endregion

		#region string R002NRLogRadOurO

		private string _r002nrlOgRadOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_r002nrlOgRadOurO", Name = "R002_NR_LOGRADOURO", DbType = "varchar(15)")]
		public string R002NRLogRadOurO
		{
			get
			{
				return _r002nrlOgRadOurO;
			}
			set
			{
				if (value != _r002nrlOgRadOurO)
				{
					_r002nrlOgRadOurO = value;
					OnPropertyChanged("R002NRLogRadOurO");
				}
			}
		}

		#endregion

		#region int? T001SQPeSsOA

		private int? _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int")]
		public int? T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region int? T001SQPeSSoapAi

		private int? _t001sqpESsOapAi;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOapAi", Name = "T001_SQ_PESSOA_PAI", DbType = "int")]
		public int? T001SQPeSSoapAi
		{
			get
			{
				return _t001sqpESsOapAi;
			}
			set
			{
				if (value != _t001sqpESsOapAi)
				{
					_t001sqpESsOapAi = value;
					OnPropertyChanged("T001SQPeSSoapAi");
				}
			}
		}

		#endregion

		#region ctor

		public R002VInCuLoEnderEcO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.r003_rel_org_munic")]
	public partial class R003ReLOrGMuNiC : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A005ComUnICipIo

		private int _a005cOmUnIcIpIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a005cOmUnIcIpIo", Name = "A005_CO_MUNICIPIO", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int A005ComUnICipIo
		{
			get
			{
				return _a005cOmUnIcIpIo;
			}
			set
			{
				if (value != _a005cOmUnIcIpIo)
				{
					_a005cOmUnIcIpIo = value;
					OnPropertyChanged("A005ComUnICipIo");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)", IsPrimaryKey = true, CanBeNull = false)]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region ctor

		public R003ReLOrGMuNiC()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.r004_atuacao")]
	public partial class R004AtUAcAO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A001CoatIViDade

		private int _a001cOatIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_a001cOatIvIDade", Name = "A001_CO_ATIVIDADE", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int A001CoatIViDade
		{
			get
			{
				return _a001cOatIvIDade;
			}
			set
			{
				if (value != _a001cOatIvIDade)
				{
					_a001cOatIvIDade = value;
					OnPropertyChanged("A001CoatIViDade");
				}
			}
		}

		#endregion

		#region string R004InPrincipalsEcUnDarIo

		private string _r004iNPrincipalsEcUnDarIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_r004iNPrincipalsEcUnDarIo", Name = "R004_IN_PRINCIPAL_SECUNDARIO", DbType = "char(1)")]
		public string R004InPrincipalsEcUnDarIo
		{
			get
			{
				return _r004iNPrincipalsEcUnDarIo;
			}
			set
			{
				if (value != _r004iNPrincipalsEcUnDarIo)
				{
					_r004iNPrincipalsEcUnDarIo = value;
					OnPropertyChanged("R004InPrincipalsEcUnDarIo");
				}
			}
		}

		#endregion

		#region int T001SQPeSsOA

		private int _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region ctor

		public R004AtUAcAO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.r005_protocolo_evento")]
	public partial class R005ProtocolOEventO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int A003COeVentO

		private int _a003coEVentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a003coEVentO", Name = "A003_CO_EVENTO", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int A003COeVentO
		{
			get
			{
				return _a003coEVentO;
			}
			set
			{
				if (value != _a003coEVentO)
				{
					_a003coEVentO = value;
					OnPropertyChanged("A003COeVentO");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)", IsPrimaryKey = true, CanBeNull = false)]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region string T007NRProtocolO

		private string _t007nrpRotocolO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t007nrpRotocolO", Name = "T007_NR_PROTOCOLO", DbType = "varchar(20)", IsPrimaryKey = true, CanBeNull = false)]
		public string T007NRProtocolO
		{
			get
			{
				return _t007nrpRotocolO;
			}
			set
			{
				if (value != _t007nrpRotocolO)
				{
					_t007nrpRotocolO = value;
					OnPropertyChanged("T007NRProtocolO");
				}
			}
		}

		#endregion

		#region ctor

		public R005ProtocolOEventO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t001_pessoa")]
	public partial class T001PeSsOA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string T001DSpEsSoA

		private string _t001dsPEsSoA;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001dsPEsSoA", Name = "T001_DS_PESSOA", DbType = "varchar(200)")]
		public string T001DSpEsSoA
		{
			get
			{
				return _t001dsPEsSoA;
			}
			set
			{
				if (value != _t001dsPEsSoA)
				{
					_t001dsPEsSoA = value;
					OnPropertyChanged("T001DSpEsSoA");
				}
			}
		}

		#endregion

		#region DateTime? T001DTUlTatUALizAcAO

		private DateTime? _t001dtuLTatUalIzAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001dtuLTatUalIzAcAo", Name = "T001_DT_ULT_ATUALIZACAO", DbType = "datetime")]
		public DateTime? T001DTUlTatUALizAcAO
		{
			get
			{
				return _t001dtuLTatUalIzAcAo;
			}
			set
			{
				if (value != _t001dtuLTatUalIzAcAo)
				{
					_t001dtuLTatUalIzAcAo = value;
					OnPropertyChanged("T001DTUlTatUALizAcAO");
				}
			}
		}

		#endregion

		#region string T001Email

		private string _t001eMail;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001eMail", Name = "T001_EMAIL", DbType = "varchar(50)")]
		public string T001Email
		{
			get
			{
				return _t001eMail;
			}
			set
			{
				if (value != _t001eMail)
				{
					_t001eMail = value;
					OnPropertyChanged("T001Email");
				}
			}
		}

		#endregion

		#region string T001InDadoSatUALizAdoS

		private string _t001iNDadoSatUalIzAdoS;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001iNDadoSatUalIzAdoS", Name = "T001_IN_DADOS_ATUALIZADOS", DbType = "char(1)")]
		public string T001InDadoSatUALizAdoS
		{
			get
			{
				return _t001iNDadoSatUalIzAdoS;
			}
			set
			{
				if (value != _t001iNDadoSatUalIzAdoS)
				{
					_t001iNDadoSatUalIzAdoS = value;
					OnPropertyChanged("T001InDadoSatUALizAdoS");
				}
			}
		}

		#endregion

		#region string T001InTipOpeSsOA

		private string _t001iNTipOpeSsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001iNTipOpeSsOa", Name = "T001_IN_TIPO_PESSOA", DbType = "char(1)")]
		public string T001InTipOpeSsOA
		{
			get
			{
				return _t001iNTipOpeSsOa;
			}
			set
			{
				if (value != _t001iNTipOpeSsOa)
				{
					_t001iNTipOpeSsOa = value;
					OnPropertyChanged("T001InTipOpeSsOA");
				}
			}
		}

		#endregion

		#region int T001SQPeSsOA

		private int _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region string T001TeL1

		private string _t001tEL1;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001tEL1", Name = "T001_TEL_1", DbType = "varchar(20)")]
		public string T001TeL1
		{
			get
			{
				return _t001tEL1;
			}
			set
			{
				if (value != _t001tEL1)
				{
					_t001tEL1 = value;
					OnPropertyChanged("T001TeL1");
				}
			}
		}

		#endregion

		#region string T001TeL2

		private string _t001tEL2;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001tEL2", Name = "T001_TEL_2", DbType = "varchar(20)")]
		public string T001TeL2
		{
			get
			{
				return _t001tEL2;
			}
			set
			{
				if (value != _t001tEL2)
				{
					_t001tEL2 = value;
					OnPropertyChanged("T001TeL2");
				}
			}
		}

		#endregion

		#region ctor

		public T001PeSsOA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t002_pessoa_fisica")]
	public partial class T002PeSsOAFIsiCa : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int? A004CopAiS

		private int? _a004cOpAiS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004cOpAiS", Name = "A004_CO_PAIS", DbType = "int")]
		public int? A004CopAiS
		{
			get
			{
				return _a004cOpAiS;
			}
			set
			{
				if (value != _a004cOpAiS)
				{
					_a004cOpAiS = value;
					OnPropertyChanged("A004CopAiS");
				}
			}
		}

		#endregion

		#region int? A004COUFNaturalIDade

		private int? _a004coufnAturalIDAde;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004coufnAturalIDAde", Name = "A004_CO_UF_NATURALIDADE", DbType = "int")]
		public int? A004COUFNaturalIDade
		{
			get
			{
				return _a004coufnAturalIDAde;
			}
			set
			{
				if (value != _a004coufnAturalIDAde)
				{
					_a004coufnAturalIDAde = value;
					OnPropertyChanged("A004COUFNaturalIDade");
				}
			}
		}

		#endregion

		#region int? A010CotIPODocumentO

		private int? _a010cOtIpodOcumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a010cOtIpodOcumentO", Name = "A010_CO_TIPO_DOCUMENTO", DbType = "int")]
		public int? A010CotIPODocumentO
		{
			get
			{
				return _a010cOtIpodOcumentO;
			}
			set
			{
				if (value != _a010cOtIpodOcumentO)
				{
					_a010cOtIpodOcumentO = value;
					OnPropertyChanged("A010CotIPODocumentO");
				}
			}
		}

		#endregion

		#region int? A012COEstAdoCivil

		private int? _a012coeStAdoCivil;
		[DebuggerNonUserCode]
		[Column(Storage = "_a012coeStAdoCivil", Name = "A012_CO_ESTADO_CIVIL", DbType = "int")]
		public int? A012COEstAdoCivil
		{
			get
			{
				return _a012coeStAdoCivil;
			}
			set
			{
				if (value != _a012coeStAdoCivil)
				{
					_a012coeStAdoCivil = value;
					OnPropertyChanged("A012COEstAdoCivil");
				}
			}
		}

		#endregion

		#region int? A013CoreGiMeBenS

		private int? _a013cOreGiMeBenS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a013cOreGiMeBenS", Name = "A013_CO_REGIME_BENS", DbType = "int")]
		public int? A013CoreGiMeBenS
		{
			get
			{
				return _a013cOreGiMeBenS;
			}
			set
			{
				if (value != _a013cOreGiMeBenS)
				{
					_a013cOreGiMeBenS = value;
					OnPropertyChanged("A013CoreGiMeBenS");
				}
			}
		}

		#endregion

		#region int? A014COeManCipAcAO

		private int? _a014coEManCipAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a014coEManCipAcAo", Name = "A014_CO_EMANCIPACAO", DbType = "int")]
		public int? A014COeManCipAcAO
		{
			get
			{
				return _a014coEManCipAcAo;
			}
			set
			{
				if (value != _a014coEManCipAcAo)
				{
					_a014coEManCipAcAo = value;
					OnPropertyChanged("A014COeManCipAcAO");
				}
			}
		}

		#endregion

		#region int T001SQPeSsOA

		private int _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region DateTime? T002DSemisSoRDocumentO

		private DateTime? _t002dsEmisSoRdOcumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002dsEmisSoRdOcumentO", Name = "T002_DS_EMISSOR_DOCUMENTO", DbType = "date")]
		public DateTime? T002DSemisSoRDocumentO
		{
			get
			{
				return _t002dsEmisSoRdOcumentO;
			}
			set
			{
				if (value != _t002dsEmisSoRdOcumentO)
				{
					_t002dsEmisSoRdOcumentO = value;
					OnPropertyChanged("T002DSemisSoRDocumentO");
				}
			}
		}

		#endregion

		#region DateTime? T002DTeMisSaoDocumentO

		private DateTime? _t002dtEMisSaoDocumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002dtEMisSaoDocumentO", Name = "T002_DT_EMISSAO_DOCUMENTO", DbType = "date")]
		public DateTime? T002DTeMisSaoDocumentO
		{
			get
			{
				return _t002dtEMisSaoDocumentO;
			}
			set
			{
				if (value != _t002dtEMisSaoDocumentO)
				{
					_t002dtEMisSaoDocumentO = value;
					OnPropertyChanged("T002DTeMisSaoDocumentO");
				}
			}
		}

		#endregion

		#region DateTime? T002DTnASciMenTo

		private DateTime? _t002dtNAsCiMenTo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002dtNAsCiMenTo", Name = "T002_DT_NASCIMENTO", DbType = "date")]
		public DateTime? T002DTnASciMenTo
		{
			get
			{
				return _t002dtNAsCiMenTo;
			}
			set
			{
				if (value != _t002dtNAsCiMenTo)
				{
					_t002dtNAsCiMenTo = value;
					OnPropertyChanged("T002DTnASciMenTo");
				}
			}
		}

		#endregion

		#region string T002InSexO

		private string _t002iNSexO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002iNSexO", Name = "T002_IN_SEXO", DbType = "char(1)")]
		public string T002InSexO
		{
			get
			{
				return _t002iNSexO;
			}
			set
			{
				if (value != _t002iNSexO)
				{
					_t002iNSexO = value;
					OnPropertyChanged("T002InSexO");
				}
			}
		}

		#endregion

		#region string T002NRcPF

		private string _t002nrCPf;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002nrCPf", Name = "T002_NR_CPF", DbType = "varchar(14)")]
		public string T002NRcPF
		{
			get
			{
				return _t002nrCPf;
			}
			set
			{
				if (value != _t002nrCPf)
				{
					_t002nrCPf = value;
					OnPropertyChanged("T002NRcPF");
				}
			}
		}

		#endregion

		#region string T002NRDocumentO

		private string _t002nrdOcumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t002nrdOcumentO", Name = "T002_NR_DOCUMENTO", DbType = "varchar(30)")]
		public string T002NRDocumentO
		{
			get
			{
				return _t002nrdOcumentO;
			}
			set
			{
				if (value != _t002nrdOcumentO)
				{
					_t002nrdOcumentO = value;
					OnPropertyChanged("T002NRDocumentO");
				}
			}
		}

		#endregion

		#region ctor

		public T002PeSsOAFIsiCa()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t003_pessoa_juridica")]
	public partial class T003PeSsOAJuridicA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int? A006CONatureZAJuridicA

		private int? _a006conAtureZajUridicA;
		[DebuggerNonUserCode]
		[Column(Storage = "_a006conAtureZajUridicA", Name = "A006_CO_NATUREZA_JURIDICA", DbType = "int")]
		public int? A006CONatureZAJuridicA
		{
			get
			{
				return _a006conAtureZajUridicA;
			}
			set
			{
				if (value != _a006conAtureZajUridicA)
				{
					_a006conAtureZajUridicA = value;
					OnPropertyChanged("A006CONatureZAJuridicA");
				}
			}
		}

		#endregion

		#region int? A007CosItuAcAO

		private int? _a007cOsItuAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a007cOsItuAcAo", Name = "A007_CO_SITUACAO", DbType = "int")]
		public int? A007CosItuAcAO
		{
			get
			{
				return _a007cOsItuAcAo;
			}
			set
			{
				if (value != _a007cOsItuAcAo)
				{
					_a007cOsItuAcAo = value;
					OnPropertyChanged("A007CosItuAcAO");
				}
			}
		}

		#endregion

		#region int? A008CostaTUsAtUAL

		private int? _a008cOstaTuSAtUal;
		[DebuggerNonUserCode]
		[Column(Storage = "_a008cOstaTuSAtUal", Name = "A008_CO_STATUS_ATUAL", DbType = "int")]
		public int? A008CostaTUsAtUAL
		{
			get
			{
				return _a008cOstaTuSAtUal;
			}
			set
			{
				if (value != _a008cOstaTuSAtUal)
				{
					_a008cOstaTuSAtUal = value;
					OnPropertyChanged("A008CostaTUsAtUAL");
				}
			}
		}

		#endregion

		#region int? A011CopOrTe

		private int? _a011cOpOrTe;
		[DebuggerNonUserCode]
		[Column(Storage = "_a011cOpOrTe", Name = "A011_CO_PORTE", DbType = "int")]
		public int? A011CopOrTe
		{
			get
			{
				return _a011cOpOrTe;
			}
			set
			{
				if (value != _a011cOpOrTe)
				{
					_a011cOpOrTe = value;
					OnPropertyChanged("A011CopOrTe");
				}
			}
		}

		#endregion

		#region int T001SQPeSsOA

		private int _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int", IsPrimaryKey = true, CanBeNull = false)]
		public int T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region int? T003COFormAAtUAcAO

		private int? _t003cofOrmAaTUaCAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003cofOrmAaTUaCAo", Name = "T003_CO_FORMA_ATUACAO", DbType = "int")]
		public int? T003COFormAAtUAcAO
		{
			get
			{
				return _t003cofOrmAaTUaCAo;
			}
			set
			{
				if (value != _t003cofOrmAaTUaCAo)
				{
					_t003cofOrmAaTUaCAo = value;
					OnPropertyChanged("T003COFormAAtUAcAO");
				}
			}
		}

		#endregion

		#region int? T003CotIPopeSjUR

		private int? _t003cOtIpOpeSjUr;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003cOtIpOpeSjUr", Name = "T003_CO_TIPO_PES_JUR", DbType = "int")]
		public int? T003CotIPopeSjUR
		{
			get
			{
				return _t003cOtIpOpeSjUr;
			}
			set
			{
				if (value != _t003cOtIpOpeSjUr)
				{
					_t003cOtIpOpeSjUr = value;
					OnPropertyChanged("T003CotIPopeSjUR");
				}
			}
		}

		#endregion

		#region string T003DSobJetOSocial

		private string _t003dsObJetOsOcial;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003dsObJetOsOcial", Name = "T003_DS_OBJETO_SOCIAL", DbType = "varchar(200)")]
		public string T003DSobJetOSocial
		{
			get
			{
				return _t003dsObJetOsOcial;
			}
			set
			{
				if (value != _t003dsObJetOsOcial)
				{
					_t003dsObJetOsOcial = value;
					OnPropertyChanged("T003DSobJetOSocial");
				}
			}
		}

		#endregion

		#region DateTime? T003DTcOnSTitUIcaO

		private DateTime? _t003dtCOnStItUiCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003dtCOnStItUiCaO", Name = "T003_DT_CONSTITUICAO", DbType = "date")]
		public DateTime? T003DTcOnSTitUIcaO
		{
			get
			{
				return _t003dtCOnStItUiCaO;
			}
			set
			{
				if (value != _t003dtCOnStItUiCaO)
				{
					_t003dtCOnStItUiCaO = value;
					OnPropertyChanged("T003DTcOnSTitUIcaO");
				}
			}
		}

		#endregion

		#region DateTime? T003DTInICIoAtIViDade

		private DateTime? _t003dtiNIciOAtIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003dtiNIciOAtIvIDade", Name = "T003_DT_INICIO_ATIVIDADE", DbType = "date")]
		public DateTime? T003DTInICIoAtIViDade
		{
			get
			{
				return _t003dtiNIciOAtIvIDade;
			}
			set
			{
				if (value != _t003dtiNIciOAtIvIDade)
				{
					_t003dtiNIciOAtIvIDade = value;
					OnPropertyChanged("T003DTInICIoAtIViDade");
				}
			}
		}

		#endregion

		#region DateTime? T003DtpRaZOdeTermInAdo

		private DateTime? _t003dTpRaZoDeTermInAdo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003dTpRaZoDeTermInAdo", Name = "T003_DT_PRAZO_DETERMINADO", DbType = "date")]
		public DateTime? T003DtpRaZOdeTermInAdo
		{
			get
			{
				return _t003dTpRaZoDeTermInAdo;
			}
			set
			{
				if (value != _t003dTpRaZoDeTermInAdo)
				{
					_t003dTpRaZoDeTermInAdo = value;
					OnPropertyChanged("T003DtpRaZOdeTermInAdo");
				}
			}
		}

		#endregion

		#region DateTime? T003DTTermInOatIV

		private DateTime? _t003dttErmInOatIv;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003dttErmInOatIv", Name = "T003_DT_TERMINO_ATIV", DbType = "date")]
		public DateTime? T003DTTermInOatIV
		{
			get
			{
				return _t003dttErmInOatIv;
			}
			set
			{
				if (value != _t003dttErmInOatIv)
				{
					_t003dttErmInOatIv = value;
					OnPropertyChanged("T003DTTermInOatIV");
				}
			}
		}

		#endregion

		#region string T003NRcNPJ

		private string _t003nrCNpj;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003nrCNpj", Name = "T003_NR_CNPJ", DbType = "varchar(14)")]
		public string T003NRcNPJ
		{
			get
			{
				return _t003nrCNpj;
			}
			set
			{
				if (value != _t003nrCNpj)
				{
					_t003nrCNpj = value;
					OnPropertyChanged("T003NRcNPJ");
				}
			}
		}

		#endregion

		#region string T003NRInSCrIcaOEstADual

		private string _t003nriNScRIcaOeStAdUal;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003nriNScRIcaOeStAdUal", Name = "T003_NR_INSCRICAO_ESTADUAL", DbType = "varchar(12)")]
		public string T003NRInSCrIcaOEstADual
		{
			get
			{
				return _t003nriNScRIcaOeStAdUal;
			}
			set
			{
				if (value != _t003nriNScRIcaOeStAdUal)
				{
					_t003nriNScRIcaOeStAdUal = value;
					OnPropertyChanged("T003NRInSCrIcaOEstADual");
				}
			}
		}

		#endregion

		#region string T003NRInSCrIcaOMunicipal

		private string _t003nriNScRIcaOmUnicipal;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003nriNScRIcaOmUnicipal", Name = "T003_NR_INSCRICAO_MUNICIPAL", DbType = "varchar(12)")]
		public string T003NRInSCrIcaOMunicipal
		{
			get
			{
				return _t003nriNScRIcaOmUnicipal;
			}
			set
			{
				if (value != _t003nriNScRIcaOmUnicipal)
				{
					_t003nriNScRIcaOmUnicipal = value;
					OnPropertyChanged("T003NRInSCrIcaOMunicipal");
				}
			}
		}

		#endregion

		#region string T003NRMatRiCuLa

		private string _t003nrmAtRiCuLa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003nrmAtRiCuLa", Name = "T003_NR_MATRICULA", DbType = "varchar(20)")]
		public string T003NRMatRiCuLa
		{
			get
			{
				return _t003nrmAtRiCuLa;
			}
			set
			{
				if (value != _t003nrmAtRiCuLa)
				{
					_t003nrmAtRiCuLa = value;
					OnPropertyChanged("T003NRMatRiCuLa");
				}
			}
		}

		#endregion

		#region string T003NRMatRiCuLaAnterior

		private string _t003nrmAtRiCuLaAnterior;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003nrmAtRiCuLaAnterior", Name = "T003_NR_MATRICULA_ANTERIOR", DbType = "varchar(20)")]
		public string T003NRMatRiCuLaAnterior
		{
			get
			{
				return _t003nrmAtRiCuLaAnterior;
			}
			set
			{
				if (value != _t003nrmAtRiCuLaAnterior)
				{
					_t003nrmAtRiCuLaAnterior = value;
					OnPropertyChanged("T003NRMatRiCuLaAnterior");
				}
			}
		}

		#endregion

		#region decimal? T003VLCapital

		private decimal? _t003vlcApital;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003vlcApital", Name = "T003_VL_CAPITAL", DbType = "decimal")]
		public decimal? T003VLCapital
		{
			get
			{
				return _t003vlcApital;
			}
			set
			{
				if (value != _t003vlcApital)
				{
					_t003vlcApital = value;
					OnPropertyChanged("T003VLCapital");
				}
			}
		}

		#endregion

		#region decimal? T003VLCapitalIntegralIZAdo

		private decimal? _t003vlcApitalIntegralIzaDo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t003vlcApitalIntegralIzaDo", Name = "T003_VL_CAPITAL_INTEGRALIZADO", DbType = "decimal")]
		public decimal? T003VLCapitalIntegralIZAdo
		{
			get
			{
				return _t003vlcApitalIntegralIzaDo;
			}
			set
			{
				if (value != _t003vlcApitalIntegralIzaDo)
				{
					_t003vlcApitalIntegralIzaDo = value;
					OnPropertyChanged("T003VLCapitalIntegralIZAdo");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)")]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region string T006NRcNPJoRGregAnt

		private string _t006nrCNpjORgRegAnt;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrCNpjORgRegAnt", Name = "T006_NR_CNPJ_ORG_REG_ANT", DbType = "varchar(14)")]
		public string T006NRcNPJoRGregAnt
		{
			get
			{
				return _t006nrCNpjORgRegAnt;
			}
			set
			{
				if (value != _t006nrCNpjORgRegAnt)
				{
					_t006nrCNpjORgRegAnt = value;
					OnPropertyChanged("T006NRcNPJoRGregAnt");
				}
			}
		}

		#endregion

		#region ctor

		public T003PeSsOAJuridicA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t004_orgao_registro")]
	public partial class T004OrGaoRegisTRO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int? A004CopAiS

		private int? _a004cOpAiS;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004cOpAiS", Name = "A004_CO_PAIS", DbType = "int")]
		public int? A004CopAiS
		{
			get
			{
				return _a004cOpAiS;
			}
			set
			{
				if (value != _a004cOpAiS)
				{
					_a004cOpAiS = value;
					OnPropertyChanged("A004CopAiS");
				}
			}
		}

		#endregion

		#region int? A005ComUnICipIo

		private int? _a005cOmUnIcIpIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_a005cOmUnIcIpIo", Name = "A005_CO_MUNICIPIO", DbType = "int")]
		public int? A005ComUnICipIo
		{
			get
			{
				return _a005cOmUnIcIpIo;
			}
			set
			{
				if (value != _a005cOmUnIcIpIo)
				{
					_a005cOmUnIcIpIo = value;
					OnPropertyChanged("A005ComUnICipIo");
				}
			}
		}

		#endregion

		#region int? A015CotIPoloGradOurO

		private int? _a015cOtIpOloGradOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_a015cOtIpOloGradOurO", Name = "A015_CO_TIPO_LOGRADOURO", DbType = "int")]
		public int? A015CotIPoloGradOurO
		{
			get
			{
				return _a015cOtIpOloGradOurO;
			}
			set
			{
				if (value != _a015cOtIpOloGradOurO)
				{
					_a015cOtIpOloGradOurO = value;
					OnPropertyChanged("A015CotIPoloGradOurO");
				}
			}
		}

		#endregion

		#region string T004DSbaIrRO

		private string _t004dsBaIrRo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004dsBaIrRo", Name = "T004_DS_BAIRRO", DbType = "varchar(30)")]
		public string T004DSbaIrRO
		{
			get
			{
				return _t004dsBaIrRo;
			}
			set
			{
				if (value != _t004dsBaIrRo)
				{
					_t004dsBaIrRo = value;
					OnPropertyChanged("T004DSbaIrRO");
				}
			}
		}

		#endregion

		#region string T004DSComplementO

		private string _t004dscOmplementO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004dscOmplementO", Name = "T004_DS_COMPLEMENTO", DbType = "varchar(15)")]
		public string T004DSComplementO
		{
			get
			{
				return _t004dscOmplementO;
			}
			set
			{
				if (value != _t004dscOmplementO)
				{
					_t004dscOmplementO = value;
					OnPropertyChanged("T004DSComplementO");
				}
			}
		}

		#endregion

		#region string T004DSlogRadOurO

		private string _t004dsLogRadOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004dsLogRadOurO", Name = "T004_DS_LOGRADOURO", DbType = "varchar(60)")]
		public string T004DSlogRadOurO
		{
			get
			{
				return _t004dsLogRadOurO;
			}
			set
			{
				if (value != _t004dsLogRadOurO)
				{
					_t004dsLogRadOurO = value;
					OnPropertyChanged("T004DSlogRadOurO");
				}
			}
		}

		#endregion

		#region string T004DSoRGreg

		private string _t004dsORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004dsORgReg", Name = "T004_DS_ORG_REG", DbType = "varchar(150)")]
		public string T004DSoRGreg
		{
			get
			{
				return _t004dsORgReg;
			}
			set
			{
				if (value != _t004dsORgReg)
				{
					_t004dsORgReg = value;
					OnPropertyChanged("T004DSoRGreg");
				}
			}
		}

		#endregion

		#region string T004DSReferEnCia

		private string _t004dsrEferEnCia;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004dsrEferEnCia", Name = "T004_DS_REFERENCIA", DbType = "varchar(50)")]
		public string T004DSReferEnCia
		{
			get
			{
				return _t004dsrEferEnCia;
			}
			set
			{
				if (value != _t004dsrEferEnCia)
				{
					_t004dsrEferEnCia = value;
					OnPropertyChanged("T004DSReferEnCia");
				}
			}
		}

		#endregion

		#region string T004InOrGreg

		private string _t004iNOrGreg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004iNOrGreg", Name = "T004_IN_ORG_REG", DbType = "char(1)")]
		public string T004InOrGreg
		{
			get
			{
				return _t004iNOrGreg;
			}
			set
			{
				if (value != _t004iNOrGreg)
				{
					_t004iNOrGreg = value;
					OnPropertyChanged("T004InOrGreg");
				}
			}
		}

		#endregion

		#region string T004NRcEP

		private string _t004nrCEp;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCEp", Name = "T004_NR_CEP", DbType = "varchar(8)")]
		public string T004NRcEP
		{
			get
			{
				return _t004nrCEp;
			}
			set
			{
				if (value != _t004nrCEp)
				{
					_t004nrCEp = value;
					OnPropertyChanged("T004NRcEP");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)", IsPrimaryKey = true, CanBeNull = false)]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region string T004NRLogRadOurO

		private string _t004nrlOgRadOurO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrlOgRadOurO", Name = "T004_NR_LOGRADOURO", DbType = "varchar(15)")]
		public string T004NRLogRadOurO
		{
			get
			{
				return _t004nrlOgRadOurO;
			}
			set
			{
				if (value != _t004nrlOgRadOurO)
				{
					_t004nrlOgRadOurO = value;
					OnPropertyChanged("T004NRLogRadOurO");
				}
			}
		}

		#endregion

		#region ctor

		public T004OrGaoRegisTRO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t005_protocolo")]
	public partial class T005ProtocolO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region int? T001SQPeSsOA

		private int? _t001sqpESsOa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t001sqpESsOa", Name = "T001_SQ_PESSOA", DbType = "int")]
		public int? T001SQPeSsOA
		{
			get
			{
				return _t001sqpESsOa;
			}
			set
			{
				if (value != _t001sqpESsOa)
				{
					_t001sqpESsOa = value;
					OnPropertyChanged("T001SQPeSsOA");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)", IsPrimaryKey = true, CanBeNull = false)]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region DateTime? T005DTAverBaCaO

		private DateTime? _t005dtaVerBaCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005dtaVerBaCaO", Name = "T005_DT_AVERBACAO", DbType = "date")]
		public DateTime? T005DTAverBaCaO
		{
			get
			{
				return _t005dtaVerBaCaO;
			}
			set
			{
				if (value != _t005dtaVerBaCaO)
				{
					_t005dtaVerBaCaO = value;
					OnPropertyChanged("T005DTAverBaCaO");
				}
			}
		}

		#endregion

		#region DateTime? T005DTentRadA

		private DateTime? _t005dtEntRadA;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005dtEntRadA", Name = "T005_DT_ENTRADA", DbType = "date")]
		public DateTime? T005DTentRadA
		{
			get
			{
				return _t005dtEntRadA;
			}
			set
			{
				if (value != _t005dtEntRadA)
				{
					_t005dtEntRadA = value;
					OnPropertyChanged("T005DTentRadA");
				}
			}
		}

		#endregion

		#region string T005NRdBe

		private string _t005nrDBe;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005nrDBe", Name = "T005_NR_DBE", DbType = "varchar(20)")]
		public string T005NRdBe
		{
			get
			{
				return _t005nrDBe;
			}
			set
			{
				if (value != _t005nrDBe)
				{
					_t005nrDBe = value;
					OnPropertyChanged("T005NRdBe");
				}
			}
		}

		#endregion

		#region string T005NRdOCad

		private string _t005nrDOcAd;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005nrDOcAd", Name = "T005_NR_DOCAD", DbType = "varchar(20)")]
		public string T005NRdOCad
		{
			get
			{
				return _t005nrDOcAd;
			}
			set
			{
				if (value != _t005nrDOcAd)
				{
					_t005nrDOcAd = value;
					OnPropertyChanged("T005NRdOCad");
				}
			}
		}

		#endregion

		#region string T005NRProtocolO

		private string _t005nrpRotocolO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005nrpRotocolO", Name = "T005_NR_PROTOCOLO", DbType = "varchar(20)", IsPrimaryKey = true, CanBeNull = false)]
		public string T005NRProtocolO
		{
			get
			{
				return _t005nrpRotocolO;
			}
			set
			{
				if (value != _t005nrpRotocolO)
				{
					_t005nrpRotocolO = value;
					OnPropertyChanged("T005NRProtocolO");
				}
			}
		}

		#endregion

		#region string T005NRProtocolOViaBIlIDade

		private string _t005nrpRotocolOvIaBiLIDAde;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005nrpRotocolOvIaBiLIDAde", Name = "T005_NR_PROTOCOLO_VIABILIDADE", DbType = "varchar(20)")]
		public string T005NRProtocolOViaBIlIDade
		{
			get
			{
				return _t005nrpRotocolOvIaBiLIDAde;
			}
			set
			{
				if (value != _t005nrpRotocolOvIaBiLIDAde)
				{
					_t005nrpRotocolOvIaBiLIDAde = value;
					OnPropertyChanged("T005NRProtocolOViaBIlIDade");
				}
			}
		}

		#endregion

		#region Byte[] T005XlDbE

		private Byte[] _t005xLDbE;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005xLDbE", Name = "T005_XL_DBE", DbType = "blob")]
		public Byte[] T005XlDbE
		{
			get
			{
				return _t005xLDbE;
			}
			set
			{
				if (value != _t005xLDbE)
				{
					_t005xLDbE = value;
					OnPropertyChanged("T005XlDbE");
				}
			}
		}

		#endregion

		#region Byte[] T005XlDoCad

		private Byte[] _t005xLDoCad;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005xLDoCad", Name = "T005_XL_DOCAD", DbType = "blob")]
		public Byte[] T005XlDoCad
		{
			get
			{
				return _t005xLDoCad;
			}
			set
			{
				if (value != _t005xLDoCad)
				{
					_t005xLDoCad = value;
					OnPropertyChanged("T005XlDoCad");
				}
			}
		}

		#endregion

		#region ctor

		public T005ProtocolO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.t006_protocolo_requerimento")]
	public partial class T006ProtocolOReQUERimeNto : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string A004COUF

		private string _a004couf;
		[DebuggerNonUserCode]
		[Column(Storage = "_a004couf", Name = "A004_CO_UF", DbType = "char(2)")]
		public string A004COUF
		{
			get
			{
				return _a004couf;
			}
			set
			{
				if (value != _a004couf)
				{
					_a004couf = value;
					OnPropertyChanged("A004COUF");
				}
			}
		}

		#endregion

		#region int? A021ComOtIVOBaIXA

		private int? _a021cOmOtIvobAIxa;
		[DebuggerNonUserCode]
		[Column(Storage = "_a021cOmOtIvobAIxa", Name = "A021_CO_MOTIVO_BAIXA", DbType = "int")]
		public int? A021ComOtIVOBaIXA
		{
			get
			{
				return _a021cOmOtIvobAIxa;
			}
			set
			{
				if (value != _a021cOmOtIvobAIxa)
				{
					_a021cOmOtIvobAIxa = value;
					OnPropertyChanged("A021ComOtIVOBaIXA");
				}
			}
		}

		#endregion

		#region string T004NRcNPJoRGreg

		private string _t004nrCNpjORgReg;
		[DebuggerNonUserCode]
		[Column(Storage = "_t004nrCNpjORgReg", Name = "T004_NR_CNPJ_ORG_REG", DbType = "varchar(14)", IsPrimaryKey = true, CanBeNull = false)]
		public string T004NRcNPJoRGreg
		{
			get
			{
				return _t004nrCNpjORgReg;
			}
			set
			{
				if (value != _t004nrCNpjORgReg)
				{
					_t004nrCNpjORgReg = value;
					OnPropertyChanged("T004NRcNPJoRGreg");
				}
			}
		}

		#endregion

		#region string T005NRProtocolO

		private string _t005nrpRotocolO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t005nrpRotocolO", Name = "T005_NR_PROTOCOLO", DbType = "varchar(20)", IsPrimaryKey = true, CanBeNull = false)]
		public string T005NRProtocolO
		{
			get
			{
				return _t005nrpRotocolO;
			}
			set
			{
				if (value != _t005nrpRotocolO)
				{
					_t005nrpRotocolO = value;
					OnPropertyChanged("T005NRProtocolO");
				}
			}
		}

		#endregion

		#region string T006DSArtIGoesTatUtO

		private string _t006dsaRtIgOesTatUtO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsaRtIgOesTatUtO", Name = "T006_DS_ARTIGO_ESTATUTO", DbType = "varchar(70)")]
		public string T006DSArtIGoesTatUtO
		{
			get
			{
				return _t006dsaRtIgOesTatUtO;
			}
			set
			{
				if (value != _t006dsaRtIgOesTatUtO)
				{
					_t006dsaRtIgOesTatUtO = value;
					OnPropertyChanged("T006DSArtIGoesTatUtO");
				}
			}
		}

		#endregion

		#region string T006DSdEstInOpAtRimOnIo

		private string _t006dsDEstInOpAtRimOnIo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsDEstInOpAtRimOnIo", Name = "T006_DS_DESTINO_PATRIMONIO", DbType = "varchar(70)")]
		public string T006DSdEstInOpAtRimOnIo
		{
			get
			{
				return _t006dsDEstInOpAtRimOnIo;
			}
			set
			{
				if (value != _t006dsDEstInOpAtRimOnIo)
				{
					_t006dsDEstInOpAtRimOnIo = value;
					OnPropertyChanged("T006DSdEstInOpAtRimOnIo");
				}
			}
		}

		#endregion

		#region string T006DSNomeAdVOGadO

		private string _t006dsnOmeAdVogAdO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsnOmeAdVogAdO", Name = "T006_DS_NOME_ADVOGADO", DbType = "varchar(70)")]
		public string T006DSNomeAdVOGadO
		{
			get
			{
				return _t006dsnOmeAdVogAdO;
			}
			set
			{
				if (value != _t006dsnOmeAdVogAdO)
				{
					_t006dsnOmeAdVogAdO = value;
					OnPropertyChanged("T006DSNomeAdVOGadO");
				}
			}
		}

		#endregion

		#region string T006DSNomeConTadOr

		private string _t006dsnOmeConTadOr;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsnOmeConTadOr", Name = "T006_DS_NOME_CONTADOR", DbType = "varchar(70)")]
		public string T006DSNomeConTadOr
		{
			get
			{
				return _t006dsnOmeConTadOr;
			}
			set
			{
				if (value != _t006dsnOmeConTadOr)
				{
					_t006dsnOmeConTadOr = value;
					OnPropertyChanged("T006DSNomeConTadOr");
				}
			}
		}

		#endregion

		#region string T006DSQuorumAlterAcAO

		private string _t006dsqUorumAlterAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsqUorumAlterAcAo", Name = "T006_DS_QUORUM_ALTERACAO", DbType = "varchar(70)")]
		public string T006DSQuorumAlterAcAO
		{
			get
			{
				return _t006dsqUorumAlterAcAo;
			}
			set
			{
				if (value != _t006dsqUorumAlterAcAo)
				{
					_t006dsqUorumAlterAcAo = value;
					OnPropertyChanged("T006DSQuorumAlterAcAO");
				}
			}
		}

		#endregion

		#region string T006DSQuorumDeliBeRaCaO

		private string _t006dsqUorumDeliBeRaCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsqUorumDeliBeRaCaO", Name = "T006_DS_QUORUM_DELIBERACAO", DbType = "varchar(70)")]
		public string T006DSQuorumDeliBeRaCaO
		{
			get
			{
				return _t006dsqUorumDeliBeRaCaO;
			}
			set
			{
				if (value != _t006dsqUorumDeliBeRaCaO)
				{
					_t006dsqUorumDeliBeRaCaO = value;
					OnPropertyChanged("T006DSQuorumDeliBeRaCaO");
				}
			}
		}

		#endregion

		#region string T006DSQuorumDissOLuCaO

		private string _t006dsqUorumDissOlUCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dsqUorumDissOlUCaO", Name = "T006_DS_QUORUM_DISSOLUCAO", DbType = "varchar(70)")]
		public string T006DSQuorumDissOLuCaO
		{
			get
			{
				return _t006dsqUorumDissOlUCaO;
			}
			set
			{
				if (value != _t006dsqUorumDissOlUCaO)
				{
					_t006dsqUorumDissOlUCaO = value;
					OnPropertyChanged("T006DSQuorumDissOLuCaO");
				}
			}
		}

		#endregion

		#region DateTime? T006DTdEcRetAcAOfAleNCia

		private DateTime? _t006dtDEcRetAcAoFAleNcIa;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dtDEcRetAcAoFAleNcIa", Name = "T006_DT_DECRETACAO_FALENCIA", DbType = "datetime")]
		public DateTime? T006DTdEcRetAcAOfAleNCia
		{
			get
			{
				return _t006dtDEcRetAcAoFAleNcIa;
			}
			set
			{
				if (value != _t006dtDEcRetAcAoFAleNcIa)
				{
					_t006dtDEcRetAcAoFAleNcIa = value;
					OnPropertyChanged("T006DTdEcRetAcAOfAleNCia");
				}
			}
		}

		#endregion

		#region DateTime? T006DTInICIoLiquidAcAO

		private DateTime? _t006dtiNIciOLiquidAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dtiNIciOLiquidAcAo", Name = "T006_DT_INICIO_LIQUIDACAO", DbType = "datetime")]
		public DateTime? T006DTInICIoLiquidAcAO
		{
			get
			{
				return _t006dtiNIciOLiquidAcAo;
			}
			set
			{
				if (value != _t006dtiNIciOLiquidAcAo)
				{
					_t006dtiNIciOLiquidAcAo = value;
					OnPropertyChanged("T006DTInICIoLiquidAcAO");
				}
			}
		}

		#endregion

		#region DateTime? T006DTintErrUpcAOatIViDade

		private DateTime? _t006dtIntErrUpcAoAtIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dtIntErrUpcAoAtIvIDade", Name = "T006_DT_INTERRUPCAO_ATIVIDADE", DbType = "datetime")]
		public DateTime? T006DTintErrUpcAOatIViDade
		{
			get
			{
				return _t006dtIntErrUpcAoAtIvIDade;
			}
			set
			{
				if (value != _t006dtIntErrUpcAoAtIvIDade)
				{
					_t006dtIntErrUpcAoAtIvIDade = value;
					OnPropertyChanged("T006DTintErrUpcAOatIViDade");
				}
			}
		}

		#endregion

		#region DateTime? T006DTReInICIoAtIVidaDes

		private DateTime? _t006dtrEInIciOAtIvIdaDes;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dtrEInIciOAtIvIdaDes", Name = "T006_DT_REINICIO_ATIVIDADES", DbType = "datetime")]
		public DateTime? T006DTReInICIoAtIVidaDes
		{
			get
			{
				return _t006dtrEInIciOAtIvIdaDes;
			}
			set
			{
				if (value != _t006dtrEInIciOAtIvIdaDes)
				{
					_t006dtrEInIciOAtIvIdaDes = value;
					OnPropertyChanged("T006DTReInICIoAtIVidaDes");
				}
			}
		}

		#endregion

		#region DateTime? T006DTTermInOLiquidAcAO

		private DateTime? _t006dttErmInOlIquidAcAo;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006dttErmInOlIquidAcAo", Name = "T006_DT_TERMINO_LIQUIDACAO", DbType = "datetime")]
		public DateTime? T006DTTermInOLiquidAcAO
		{
			get
			{
				return _t006dttErmInOlIquidAcAo;
			}
			set
			{
				if (value != _t006dttErmInOlIquidAcAo)
				{
					_t006dttErmInOlIquidAcAo = value;
					OnPropertyChanged("T006DTTermInOLiquidAcAO");
				}
			}
		}

		#endregion

		#region string T006InAtAmesMOInstrumentO

		private string _t006iNAtAmesMoiNstrumentO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006iNAtAmesMoiNstrumentO", Name = "T006_IN_ATA_MESMO_INSTRUMENTO", DbType = "char(1)")]
		public string T006InAtAmesMOInstrumentO
		{
			get
			{
				return _t006iNAtAmesMoiNstrumentO;
			}
			set
			{
				if (value != _t006iNAtAmesMoiNstrumentO)
				{
					_t006iNAtAmesMoiNstrumentO = value;
					OnPropertyChanged("T006InAtAmesMOInstrumentO");
				}
			}
		}

		#endregion

		#region string T006InFontEreCursOs

		private string _t006iNFontEreCursOs;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006iNFontEreCursOs", Name = "T006_IN_FONTE_RECURSOS", DbType = "varchar(60)")]
		public string T006InFontEreCursOs
		{
			get
			{
				return _t006iNFontEreCursOs;
			}
			set
			{
				if (value != _t006iNFontEreCursOs)
				{
					_t006iNFontEreCursOs = value;
					OnPropertyChanged("T006InFontEreCursOs");
				}
			}
		}

		#endregion

		#region string T006InForMaconVOCacao

		private string _t006iNForMaconVocAcao;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006iNForMaconVocAcao", Name = "T006_IN_FORMA_CONVOCACAO", DbType = "char(1)")]
		public string T006InForMaconVOCacao
		{
			get
			{
				return _t006iNForMaconVocAcao;
			}
			set
			{
				if (value != _t006iNForMaconVocAcao)
				{
					_t006iNForMaconVocAcao = value;
					OnPropertyChanged("T006InForMaconVOCacao");
				}
			}
		}

		#endregion

		#region string T006InOBrigAcOeSsOCiaIs

		private string _t006iNObRigAcOeSsOcIaIs;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006iNObRigAcOeSsOcIaIs", Name = "T006_IN_OBRIGACOES_SOCIAIS", DbType = "char(1)")]
		public string T006InOBrigAcOeSsOCiaIs
		{
			get
			{
				return _t006iNObRigAcOeSsOcIaIs;
			}
			set
			{
				if (value != _t006iNObRigAcOeSsOcIaIs)
				{
					_t006iNObRigAcOeSsOcIaIs = value;
					OnPropertyChanged("T006InOBrigAcOeSsOCiaIs");
				}
			}
		}

		#endregion

		#region string T006InPOsSUIfUndoSocial

		private string _t006iNPoSSuiFUndoSocial;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006iNPoSSuiFUndoSocial", Name = "T006_IN_POSSUI_FUNDO_SOCIAL", DbType = "char(1)")]
		public string T006InPOsSUIfUndoSocial
		{
			get
			{
				return _t006iNPoSSuiFUndoSocial;
			}
			set
			{
				if (value != _t006iNPoSSuiFUndoSocial)
				{
					_t006iNPoSSuiFUndoSocial = value;
					OnPropertyChanged("T006InPOsSUIfUndoSocial");
				}
			}
		}

		#endregion

		#region string T006NiRe

		private string _t006nIRe;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nIRe", Name = "T006_NIRE", DbType = "varchar(20)")]
		public string T006NiRe
		{
			get
			{
				return _t006nIRe;
			}
			set
			{
				if (value != _t006nIRe)
				{
					_t006nIRe = value;
					OnPropertyChanged("T006NiRe");
				}
			}
		}

		#endregion

		#region string T006NRArtEstAtUtOasSoCiaCaO

		private string _t006nraRtEstAtUtOasSoCiaCaO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nraRtEstAtUtOasSoCiaCaO", Name = "T006_NR_ART_ESTATUTO_ASSOCIACAO", DbType = "varchar(10)")]
		public string T006NRArtEstAtUtOasSoCiaCaO
		{
			get
			{
				return _t006nraRtEstAtUtOasSoCiaCaO;
			}
			set
			{
				if (value != _t006nraRtEstAtUtOasSoCiaCaO)
				{
					_t006nraRtEstAtUtOasSoCiaCaO = value;
					OnPropertyChanged("T006NRArtEstAtUtOasSoCiaCaO");
				}
			}
		}

		#endregion

		#region string T006NRArtEstAtUtOConVOCacao

		private string _t006nraRtEstAtUtOcOnVocAcao;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nraRtEstAtUtOcOnVocAcao", Name = "T006_NR_ART_ESTATUTO_CONVOCACAO", DbType = "varchar(10)")]
		public string T006NRArtEstAtUtOConVOCacao
		{
			get
			{
				return _t006nraRtEstAtUtOcOnVocAcao;
			}
			set
			{
				if (value != _t006nraRtEstAtUtOcOnVocAcao)
				{
					_t006nraRtEstAtUtOcOnVocAcao = value;
					OnPropertyChanged("T006NRArtEstAtUtOConVOCacao");
				}
			}
		}

		#endregion

		#region string T006NRcNPJSUCeSsOrA

		private string _t006nrCNpjsucESsOrA;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrCNpjsucESsOrA", Name = "T006_NR_CNPJ_SUCESSORA", DbType = "varchar(14)")]
		public string T006NRcNPJSUCeSsOrA
		{
			get
			{
				return _t006nrCNpjsucESsOrA;
			}
			set
			{
				if (value != _t006nrCNpjsucESsOrA)
				{
					_t006nrCNpjsucESsOrA = value;
					OnPropertyChanged("T006NRcNPJSUCeSsOrA");
				}
			}
		}

		#endregion

		#region string T006NRcPFadVOGadO

		private string _t006nrCPfAdVogAdO;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrCPfAdVogAdO", Name = "T006_NR_CPF_ADVOGADO", DbType = "varchar(11)")]
		public string T006NRcPFadVOGadO
		{
			get
			{
				return _t006nrCPfAdVogAdO;
			}
			set
			{
				if (value != _t006nrCPfAdVogAdO)
				{
					_t006nrCPfAdVogAdO = value;
					OnPropertyChanged("T006NRcPFadVOGadO");
				}
			}
		}

		#endregion

		#region string T006NRcPfcOnTadOr

		private string _t006nrCPfcOnTadOr;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrCPfcOnTadOr", Name = "T006_NR_CPF_CONTADOR", DbType = "varchar(11)")]
		public string T006NRcPfcOnTadOr
		{
			get
			{
				return _t006nrCPfcOnTadOr;
			}
			set
			{
				if (value != _t006nrCPfcOnTadOr)
				{
					_t006nrCPfcOnTadOr = value;
					OnPropertyChanged("T006NRcPfcOnTadOr");
				}
			}
		}

		#endregion

		#region string T006NRcRcConTadOr

		private string _t006nrCRcConTadOr;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrCRcConTadOr", Name = "T006_NR_CRC_CONTADOR", DbType = "varchar(20)")]
		public string T006NRcRcConTadOr
		{
			get
			{
				return _t006nrCRcConTadOr;
			}
			set
			{
				if (value != _t006nrCRcConTadOr)
				{
					_t006nrCRcConTadOr = value;
					OnPropertyChanged("T006NRcRcConTadOr");
				}
			}
		}

		#endregion

		#region int? T006NRFundAdoresDireToreS

		private int? _t006nrfUndAdoresDireToreS;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrfUndAdoresDireToreS", Name = "T006_NR_FUNDADORES_DIRETORES", DbType = "int")]
		public int? T006NRFundAdoresDireToreS
		{
			get
			{
				return _t006nrfUndAdoresDireToreS;
			}
			set
			{
				if (value != _t006nrfUndAdoresDireToreS)
				{
					_t006nrfUndAdoresDireToreS = value;
					OnPropertyChanged("T006NRFundAdoresDireToreS");
				}
			}
		}

		#endregion

		#region string T006NRInSCrOAB

		private string _t006nriNScROab;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nriNScROab", Name = "T006_NR_INSCR_OAB", DbType = "varchar(20)")]
		public string T006NRInSCrOAB
		{
			get
			{
				return _t006nriNScROab;
			}
			set
			{
				if (value != _t006nriNScROab)
				{
					_t006nriNScROab = value;
					OnPropertyChanged("T006NRInSCrOAB");
				}
			}
		}

		#endregion

		#region int? T006NRNumPaGInAs

		private int? _t006nrnUmPaGiNAs;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrnUmPaGiNAs", Name = "T006_NR_NUM_PAGINAS", DbType = "int")]
		public int? T006NRNumPaGInAs
		{
			get
			{
				return _t006nrnUmPaGiNAs;
			}
			set
			{
				if (value != _t006nrnUmPaGiNAs)
				{
					_t006nrnUmPaGiNAs = value;
					OnPropertyChanged("T006NRNumPaGInAs");
				}
			}
		}

		#endregion

		#region string T006NRSUCeSsOrA

		private string _t006nrsucESsOrA;
		[DebuggerNonUserCode]
		[Column(Storage = "_t006nrsucESsOrA", Name = "T006_NR_SUCESSORA", DbType = "varchar(15)")]
		public string T006NRSUCeSsOrA
		{
			get
			{
				return _t006nrsucESsOrA;
			}
			set
			{
				if (value != _t006nrsucESsOrA)
				{
					_t006nrsucESsOrA = value;
					OnPropertyChanged("T006NRSUCeSsOrA");
				}
			}
		}

		#endregion

		#region ctor

		public T006ProtocolOReQUERimeNto()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_actv_desc")]
	public partial class TabActVDesC : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string TadCodaTIViDade

		private string _tadCodaTivIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_tadCodaTivIDade", Name = "TAD_COD_ATIVIDADE", DbType = "varchar(7)", CanBeNull = false)]
		public string TadCodaTIViDade
		{
			get
			{
				return _tadCodaTivIDade;
			}
			set
			{
				if (value != _tadCodaTivIDade)
				{
					_tadCodaTivIDade = value;
					OnPropertyChanged("TadCodaTIViDade");
				}
			}
		}

		#endregion

		#region string TadDesCatIViDade

		private string _tadDesCatIvIDade;
		[DebuggerNonUserCode]
		[Column(Storage = "_tadDesCatIvIDade", Name = "TAD_DESC_ATIVIDADE", DbType = "varchar(500)")]
		public string TadDesCatIViDade
		{
			get
			{
				return _tadDesCatIvIDade;
			}
			set
			{
				if (value != _tadDesCatIvIDade)
				{
					_tadDesCatIvIDade = value;
					OnPropertyChanged("TadDesCatIViDade");
				}
			}
		}

		#endregion

		#region decimal? TadSEQUEnCia

		private decimal? _tadSequeNCia;
		[DebuggerNonUserCode]
		[Column(Storage = "_tadSequeNCia", Name = "TAD_SEQUENCIA", DbType = "decimal(3,0)")]
		public decimal? TadSEQUEnCia
		{
			get
			{
				return _tadSequeNCia;
			}
			set
			{
				if (value != _tadSequeNCia)
				{
					_tadSequeNCia = value;
					OnPropertyChanged("TadSEQUEnCia");
				}
			}
		}

		#endregion

		#region string TadTInCNPJ

		private string _tadTiNCnpj;
		[DebuggerNonUserCode]
		[Column(Storage = "_tadTiNCnpj", Name = "TAD_TIN_CNPJ", DbType = "char(14)")]
		public string TadTInCNPJ
		{
			get
			{
				return _tadTiNCnpj;
			}
			set
			{
				if (value != _tadTiNCnpj)
				{
					_tadTiNCnpj = value;
					OnPropertyChanged("TadTInCNPJ");
				}
			}
		}

		#endregion

		#region ctor

		public TabActVDesC()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_actv_econ")]
	public partial class TabActVEcOn : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string TAECodaCtVd

		private string _taecOdaCtVd;
		[DebuggerNonUserCode]
		[Column(Storage = "_taecOdaCtVd", Name = "TAE_COD_ACTVD", DbType = "varchar(7)", IsPrimaryKey = true, CanBeNull = false)]
		public string TAECodaCtVd
		{
			get
			{
				return _taecOdaCtVd;
			}
			set
			{
				if (value != _taecOdaCtVd)
				{
					_taecOdaCtVd = value;
					OnPropertyChanged("TAECodaCtVd");
				}
			}
		}

		#endregion

		#region string TAEdEsC

		private string _taeDEsC;
		[DebuggerNonUserCode]
		[Column(Storage = "_taeDEsC", Name = "TAE_DESC", DbType = "varchar(250)")]
		public string TAEdEsC
		{
			get
			{
				return _taeDEsC;
			}
			set
			{
				if (value != _taeDEsC)
				{
					_taeDEsC = value;
					OnPropertyChanged("TAEdEsC");
				}
			}
		}

		#endregion

		#region decimal? TAEdEsCadICIonAL

		private decimal? _taeDEsCadIciOnAl;
		[DebuggerNonUserCode]
		[Column(Storage = "_taeDEsCadIciOnAl", Name = "TAE_DESC_ADICIONAL", DbType = "decimal(1,0)")]
		public decimal? TAEdEsCadICIonAL
		{
			get
			{
				return _taeDEsCadIciOnAl;
			}
			set
			{
				if (value != _taeDEsCadIciOnAl)
				{
					_taeDEsCadIciOnAl = value;
					OnPropertyChanged("TAEdEsCadICIonAL");
				}
			}
		}

		#endregion

		#region int? TAEflAgMeI

		private int? _taeFlAgMeI;
		[DebuggerNonUserCode]
		[Column(Storage = "_taeFlAgMeI", Name = "TAE_FLAG_MEI", DbType = "int")]
		public int? TAEflAgMeI
		{
			get
			{
				return _taeFlAgMeI;
			}
			set
			{
				if (value != _taeFlAgMeI)
				{
					_taeFlAgMeI = value;
					OnPropertyChanged("TAEflAgMeI");
				}
			}
		}

		#endregion

		#region int? TAEflAgMeIForaLei

		private int? _taeFlAgMeIfOraLei;
		[DebuggerNonUserCode]
		[Column(Storage = "_taeFlAgMeIfOraLei", Name = "TAE_FLAG_MEI_FORA_LEI", DbType = "int")]
		public int? TAEflAgMeIForaLei
		{
			get
			{
				return _taeFlAgMeIfOraLei;
			}
			set
			{
				if (value != _taeFlAgMeIfOraLei)
				{
					_taeFlAgMeIfOraLei = value;
					OnPropertyChanged("TAEflAgMeIForaLei");
				}
			}
		}

		#endregion

		#region decimal? TAEStatus

		private decimal? _taesTatus;
		[DebuggerNonUserCode]
		[Column(Storage = "_taesTatus", Name = "TAE_STATUS", DbType = "decimal(1,0)")]
		public decimal? TAEStatus
		{
			get
			{
				return _taesTatus;
			}
			set
			{
				if (value != _taesTatus)
				{
					_taesTatus = value;
					OnPropertyChanged("TAEStatus");
				}
			}
		}

		#endregion

		#region string TAETAfFormatIV

		private string _taetaFFormatIv;
		[DebuggerNonUserCode]
		[Column(Storage = "_taetaFFormatIv", Name = "TAE_TAF_FORM_ATIV", DbType = "varchar(5)")]
		public string TAETAfFormatIV
		{
			get
			{
				return _taetaFFormatIv;
			}
			set
			{
				if (value != _taetaFFormatIv)
				{
					_taetaFFormatIv = value;
					OnPropertyChanged("TAETAfFormatIV");
				}
			}
		}

		#endregion

		#region string TAEverSao

		private string _taeVerSao;
		[DebuggerNonUserCode]
		[Column(Storage = "_taeVerSao", Name = "TAE_VERSAO", DbType = "varchar(3)")]
		public string TAEverSao
		{
			get
			{
				return _taeVerSao;
			}
			set
			{
				if (value != _taeVerSao)
				{
					_taeVerSao = value;
					OnPropertyChanged("TAEverSao");
				}
			}
		}

		#endregion

		#region ctor

		public TabActVEcOn()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_cep_tipo")]
	public partial class TabCePTipO : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string TTIABReV

		private string _ttiabrEV;
		[DebuggerNonUserCode]
		[Column(Storage = "_ttiabrEV", Name = "TTI_ABREV", DbType = "varchar(50)")]
		public string TTIABReV
		{
			get
			{
				return _ttiabrEV;
			}
			set
			{
				if (value != _ttiabrEV)
				{
					_ttiabrEV = value;
					OnPropertyChanged("TTIABReV");
				}
			}
		}

		#endregion

		#region decimal TTicLaV

		private decimal _ttIcLaV;
		[DebuggerNonUserCode]
		[Column(Storage = "_ttIcLaV", Name = "TTI_CLAV", DbType = "decimal", IsPrimaryKey = true, CanBeNull = false)]
		public decimal TTicLaV
		{
			get
			{
				return _ttIcLaV;
			}
			set
			{
				if (value != _ttIcLaV)
				{
					_ttIcLaV = value;
					OnPropertyChanged("TTicLaV");
				}
			}
		}

		#endregion

		#region string TTInOmE

		private string _ttiNOmE;
		[DebuggerNonUserCode]
		[Column(Storage = "_ttiNOmE", Name = "TTI_NOME", DbType = "varchar(50)", CanBeNull = false)]
		public string TTInOmE
		{
			get
			{
				return _ttiNOmE;
			}
			set
			{
				if (value != _ttiNOmE)
				{
					_ttiNOmE = value;
					OnPropertyChanged("TTInOmE");
				}
			}
		}

		#endregion

		#region string TTIoRigEm

		private string _ttiORigEm;
		[DebuggerNonUserCode]
		[Column(Storage = "_ttiORigEm", Name = "TTI_ORIGEM", DbType = "char(1)")]
		public string TTIoRigEm
		{
			get
			{
				return _ttiORigEm;
			}
			set
			{
				if (value != _ttiORigEm)
				{
					_ttiORigEm = value;
					OnPropertyChanged("TTIoRigEm");
				}
			}
		}

		#endregion

		#region ctor

		public TabCePTipO()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_cep_uf")]
	public partial class TabCePUF : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region string TUFNome

		private string _tufnOme;
		[DebuggerNonUserCode]
		[Column(Storage = "_tufnOme", Name = "TUF_NOME", DbType = "varchar(30)")]
		public string TUFNome
		{
			get
			{
				return _tufnOme;
			}
			set
			{
				if (value != _tufnOme)
				{
					_tufnOme = value;
					OnPropertyChanged("TUFNome");
				}
			}
		}

		#endregion

		#region string TUFUF

		private string _tufuf;
		[DebuggerNonUserCode]
		[Column(Storage = "_tufuf", Name = "TUF_UF", DbType = "varchar(2)", IsPrimaryKey = true, CanBeNull = false)]
		public string TUFUF
		{
			get
			{
				return _tufuf;
			}
			set
			{
				if (value != _tufuf)
				{
					_tufuf = value;
					OnPropertyChanged("TUFUF");
				}
			}
		}

		#endregion

		#region ctor

		public TabCePUF()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_generica")]
	public partial class TabGenericA : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region decimal TGeCodTipTab

		private decimal _tgECodTipTab;
		[DebuggerNonUserCode]
		[Column(Storage = "_tgECodTipTab", Name = "TGE_COD_TIP_TAB", DbType = "decimal(5,0)", CanBeNull = false)]
		public decimal TGeCodTipTab
		{
			get
			{
				return _tgECodTipTab;
			}
			set
			{
				if (value != _tgECodTipTab)
				{
					_tgECodTipTab = value;
					OnPropertyChanged("TGeCodTipTab");
				}
			}
		}

		#endregion

		#region DateTime? TGeFeCActual

		private DateTime? _tgEFeCaCtual;
		[DebuggerNonUserCode]
		[Column(Storage = "_tgEFeCaCtual", Name = "TGE_FEC_ACTUAL", DbType = "datetime")]
		public DateTime? TGeFeCActual
		{
			get
			{
				return _tgEFeCaCtual;
			}
			set
			{
				if (value != _tgEFeCaCtual)
				{
					_tgEFeCaCtual = value;
					OnPropertyChanged("TGeFeCActual");
				}
			}
		}

		#endregion

		#region int TGeIDTAbelA

		private int _tgEIdtaBelA;
		[DebuggerNonUserCode]
		[Column(Storage = "_tgEIdtaBelA", Name = "TGE_ID_TABELA", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int TGeIDTAbelA
		{
			get
			{
				return _tgEIdtaBelA;
			}
			set
			{
				if (value != _tgEIdtaBelA)
				{
					_tgEIdtaBelA = value;
					OnPropertyChanged("TGeIDTAbelA");
				}
			}
		}

		#endregion

		#region string TGenOmBDesC

		private string _tgEnOmBdEsC;
		[DebuggerNonUserCode]
		[Column(Storage = "_tgEnOmBdEsC", Name = "TGE_NOMB_DESC", DbType = "varchar(120)", CanBeNull = false)]
		public string TGenOmBDesC
		{
			get
			{
				return _tgEnOmBdEsC;
			}
			set
			{
				if (value != _tgEnOmBdEsC)
				{
					_tgEnOmBdEsC = value;
					OnPropertyChanged("TGenOmBDesC");
				}
			}
		}

		#endregion

		#region decimal TGeTipTab

		private decimal _tgETipTab;
		[DebuggerNonUserCode]
		[Column(Storage = "_tgETipTab", Name = "TGE_TIP_TAB", DbType = "decimal(5,0)", CanBeNull = false)]
		public decimal TGeTipTab
		{
			get
			{
				return _tgETipTab;
			}
			set
			{
				if (value != _tgETipTab)
				{
					_tgETipTab = value;
					OnPropertyChanged("TGeTipTab");
				}
			}
		}

		#endregion

		#region ctor

		public TabGenericA()
		{
		}

		#endregion

	}

	[Table(Name = "requerimento.tab_munic")]
	public partial class TabMuNiC : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged handling

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region decimal? TMuCodCatG

		private decimal? _tmUCodCatG;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUCodCatG", Name = "TMU_COD_CATG", DbType = "decimal(2,0)")]
		public decimal? TMuCodCatG
		{
			get
			{
				return _tmUCodCatG;
			}
			set
			{
				if (value != _tmUCodCatG)
				{
					_tmUCodCatG = value;
					OnPropertyChanged("TMuCodCatG");
				}
			}
		}

		#endregion

		#region decimal? TMuCodCeLEsC

		private decimal? _tmUCodCeLeSC;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUCodCeLeSC", Name = "TMU_COD_CELESC", DbType = "decimal(6,0)")]
		public decimal? TMuCodCeLEsC
		{
			get
			{
				return _tmUCodCeLeSC;
			}
			set
			{
				if (value != _tmUCodCeLeSC)
				{
					_tmUCodCeLeSC = value;
					OnPropertyChanged("TMuCodCeLEsC");
				}
			}
		}

		#endregion

		#region decimal? TMuCodCOrrEIoS

		private decimal? _tmUCodCoRrEiOS;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUCodCoRrEiOS", Name = "TMU_COD_CORREIOS", DbType = "decimal(8,0)")]
		public decimal? TMuCodCOrrEIoS
		{
			get
			{
				return _tmUCodCoRrEiOS;
			}
			set
			{
				if (value != _tmUCodCoRrEiOS)
				{
					_tmUCodCoRrEiOS = value;
					OnPropertyChanged("TMuCodCOrrEIoS");
				}
			}
		}

		#endregion

		#region decimal TMuCodMuN

		private decimal _tmUCodMuN;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUCodMuN", Name = "TMU_COD_MUN", DbType = "decimal(6,0)", IsPrimaryKey = true, CanBeNull = false)]
		public decimal TMuCodMuN
		{
			get
			{
				return _tmUCodMuN;
			}
			set
			{
				if (value != _tmUCodMuN)
				{
					_tmUCodMuN = value;
					OnPropertyChanged("TMuCodMuN");
				}
			}
		}

		#endregion

		#region string TMuNoMmUn

		private string _tmUNoMmUn;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUNoMmUn", Name = "TMU_NOM_MUN", DbType = "varchar(60)")]
		public string TMuNoMmUn
		{
			get
			{
				return _tmUNoMmUn;
			}
			set
			{
				if (value != _tmUNoMmUn)
				{
					_tmUNoMmUn = value;
					OnPropertyChanged("TMuNoMmUn");
				}
			}
		}

		#endregion

		#region string TMuNumCeP

		private string _tmUNumCeP;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUNumCeP", Name = "TMU_NUM_CEP", DbType = "varchar(8)")]
		public string TMuNumCeP
		{
			get
			{
				return _tmUNumCeP;
			}
			set
			{
				if (value != _tmUNumCeP)
				{
					_tmUNumCeP = value;
					OnPropertyChanged("TMuNumCeP");
				}
			}
		}

		#endregion

		#region string TMuTUFUF

		private string _tmUTufuf;
		[DebuggerNonUserCode]
		[Column(Storage = "_tmUTufuf", Name = "TMU_TUF_UF", DbType = "varchar(2)", IsPrimaryKey = true, CanBeNull = false)]
		public string TMuTUFUF
		{
			get
			{
				return _tmUTufuf;
			}
			set
			{
				if (value != _tmUTufuf)
				{
					_tmUTufuf = value;
					OnPropertyChanged("TMuTUFUF");
				}
			}
		}

		#endregion

		#region ctor

		public TabMuNiC()
		{
		}

		#endregion

	}
}
