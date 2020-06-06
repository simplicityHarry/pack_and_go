# Pack&Go Plugin

You may want to show your EB GUIDE model to someone who does not have the EB GUIDE software or
who does not want to set up the complete EB GUIDE simulation environment. Pack&Go provides a simple
way of delivering your model as an executable demo to anyone who does not wish to install EB GUIDE.
The Pack&Go file contains your model in binary format and an executable EB GUIDE GTF.

Pack&Go can only pack models that generate successfully. For example, if your model has errors, EB GUIDE Studio cannot export the model and Pack&go cannot pack it.

---
**NOTE**

Larger models take longer to pack.
The larger your model is, the longer the packing process takes..

---


## Using instructions

### Prerequisite

* Visual Studio 2017 version 15.9 or later is installed
* EB GUIDE Studio is installed
* The source code of this extension example is locally available

### Setup

1. In Visual Studio, open the _EB\_GUIDE\_Studio\\EB\_GUIDE\_Studio\_examples.sln_ solution file.
2. Compile the _PackAndGo_ project.\
The _PackAndGoPlugin_ library file is created. 

### Run

1. Copy the _PackAndGo.dll_ into _$GUIDE\_INSTALL\_PATH\\studio\\lib\\ui\\_.
2. Start EB GUIDE Studio.
3. Create a new EB GUIDE project or load an existing project.
4. Select the Pack&Go menu item from the menu bar.

