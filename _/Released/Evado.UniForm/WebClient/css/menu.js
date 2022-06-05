function swap(){this.className="msieFix";}
function swapBack(){this.className="trigger";}
function toggle(){(this.parentNode.className=="trigger")?this.parentNode.className="msieFix":this.parentNode.className="trigger";return false;}
function reveal(){this.parentNode.parentNode.parentNode.className="msieFix";}

function cleanUp(){
	var zA;
	var LI = document.getElementsByTagName("li");
	var zLI= LI.length;
		for(var k=0;k<zLI;k++){
		if(LI[k]!=this.parentNode){
		LI[k].className="trigger";
		}
	}
}