using System;
using System.Collections;

namespace OME.Storage
{
	public class ContainerCollection
	{
		//Container collection represents individual container
		//For example all regular orders for MSFT will be arranged 
		//in a separate container, similarly all buy orders falling under
		//regular order category will form a separate container but with 
		//reference to its parent container which is "regular order container"
		Hashtable contCollection = new Hashtable();

		//Check for existence of specific container
		public bool Exists(string containerName)
		{
			return contCollection.ContainsKey(containerName);
		}

		//Get reference to specific Container
		public Container this[string name]
		{
			get{return contCollection[name] as Container;}
			set{contCollection[name]=value;}
		}

	}
}
