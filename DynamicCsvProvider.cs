using System;
using System.Collections.Generic;
using System.Linq;

public class CSVLineInfo:System.Dynamic.DynamicObject{
	
	public List<string> HeaderContent{get;set;}
	public List<string> LineContent{get;set;}
	
	public override  bool TryGetMember (System.Dynamic.GetMemberBinder binder, out object result){
	
		string headerColumn=binder.Name;
		int index=HeaderContent.IndexOf(headerColumn);
		result=LineContent[index];
		return true;
		
	}
	
	
}

public class CSVDataProvider{
	
	string filePath;
	public string FilePath{ set { this.filePath=value;}}
	public IEnumerable<CSVLineInfo> GetAllRecords(){
		
		//Parse the File 
		//Etract Line By Line
		//Construct CSVLineInfo  Object
		//set HeaderContent and LineContent
	
		List<CSVLineInfo> _objectList=new  List<CSVLineInfo>();
	//Add CsvLineInfo Obejct
		return _objectList;
	}
		
	
}
public class Program
{
	public static void Main()
	{
	
		
	
		
		CSVDataProvider _provider=new CSVDataProvider();
		_provider.FilePath="patients.csv";
		IEnumerable<CSVLineInfo> patients=_provider.GetAllRecords();
		
		IEnumerable<dynamic> result=System.Linq.Enumerable.Where(patients, (dynamic patient)=>{  return patient.City=="BLR";});
		foreach(dynamic patient in result){
			Console.WriteLine(patient.MRN);
		}
	}
}
