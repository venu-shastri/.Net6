using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
					
public class PatientInfo{

public string MRN{get;set;}
public string Name{get;set;},
public string Age{get;set;}
public string ContactNumber{get;set;}
public string City{get;set;}
public override string ToString(){
   return this.MRN +","+this.Name+","+this.Age;
 }
 }

public class PaientCSVDataProvider{
	
	string filePath;
	public string FilePath{ set { this.filePath=value;}}
	public IEnumerable<PatientInfo> GetAllRecords(){
		
		//Parse the File 
		//Etract Lini By Line
		//Construct PatientInfo  Object
	
		List<PatientInfo> _objectList=new  List<PatientInfo>();
		_objectList.AddRange(new PatientInfo[]{});
		return _objectList;
	}
		
	
}
public class Program
{
	public static void Main()
	{
		
		PaientCSVDataProvider _provider=new PaientCSVDataProvider();
		_provider.FilePath="patients.csv";
		IEnumerable<PatientInfo> patients=_provider.GetAllRecords();
		
		IEnumerable<PatientInfo> result=System.Linq.Enumerable.Where(patients, (PatientInfo patient)=>{  return patient.CITY=="BLR";});
		foreach(PatientInfo patient in result){
			Console.WriteLine(patient.ToString());
		}
	}
