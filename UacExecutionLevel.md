# Application Manifest - UAC #

You can request privileges in your application manifest:



&lt;assembly&gt;


> 

&lt;trustInfo&gt;


> > 

&lt;security&gt;


> > > 

&lt;requestedPrivileges&gt;


> > > > <!-- UAC Manifest Options
> > > > > If you want to change the Windows User Account Control level replace the
> > > > > requestedExecutionLevel node with one of the following.


> 

&lt;requestedExecutionLevel  level="asInvoker" uiAccess="false" /&gt;


> 

&lt;requestedExecutionLevel  level="requireAdministrator" uiAccess="false" /&gt;


> 

&lt;requestedExecutionLevel  level="highestAvailable" uiAccess="false" /&gt;



[MSDN](http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k(%22URN%3aSCHEMAS-MICROSOFT-COM%3aASM.V3%23REQUESTEDPRIVILEGES%22);k(%22URN%3aSCHEMAS-MICROSOFT-COM%3aASM.V2%23SECURITY%22);k(VS.XMLEDITOR);k(SOLUTIONITEMSPROJECT);k(SOLUTIONITEMSPROJECT);k(TargetFrameworkMoniker-%22.NETFRAMEWORK%2cVERSION%3dV4.0%22)&rd=true)

  * asInvoker - requesting no additional permissions. This level requires no additional trust prompts

  * highestAvailable, requesting the highest permissions available to the parent process. (Applicable to e.g. `BackupOperators`)

  * requireAdministrator, requesting full administrator permissions.