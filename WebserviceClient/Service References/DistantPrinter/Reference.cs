﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebserviceClient.DistantPrinter {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://127.0.0.1/", ConfigurationName="DistantPrinter.PrinterSoap")]
    public interface PrinterSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://127.0.0.1/Ping", ReplyAction="*")]
        bool Ping();
        
        // CODEGEN : La génération du contrat de message depuis le nom d'élément nom de l'espace de noms http://127.0.0.1/ n'est pas marqué nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://127.0.0.1/Print", ReplyAction="*")]
        WebserviceClient.DistantPrinter.PrintResponse Print(WebserviceClient.DistantPrinter.PrintRequest request);
        
        // CODEGEN : La génération du contrat de message depuis le nom d'élément StatusResult de l'espace de noms http://127.0.0.1/ n'est pas marqué nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://127.0.0.1/Status", ReplyAction="*")]
        WebserviceClient.DistantPrinter.StatusResponse Status(WebserviceClient.DistantPrinter.StatusRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PrintRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Print", Namespace="http://127.0.0.1/", Order=0)]
        public WebserviceClient.DistantPrinter.PrintRequestBody Body;
        
        public PrintRequest() {
        }
        
        public PrintRequest(WebserviceClient.DistantPrinter.PrintRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://127.0.0.1/")]
    public partial class PrintRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int taille;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string nom;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int copies;
        
        public PrintRequestBody() {
        }
        
        public PrintRequestBody(int taille, string nom, int copies) {
            this.taille = taille;
            this.nom = nom;
            this.copies = copies;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PrintResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PrintResponse", Namespace="http://127.0.0.1/", Order=0)]
        public WebserviceClient.DistantPrinter.PrintResponseBody Body;
        
        public PrintResponse() {
        }
        
        public PrintResponse(WebserviceClient.DistantPrinter.PrintResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://127.0.0.1/")]
    public partial class PrintResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int PrintResult;
        
        public PrintResponseBody() {
        }
        
        public PrintResponseBody(int PrintResult) {
            this.PrintResult = PrintResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class StatusRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Status", Namespace="http://127.0.0.1/", Order=0)]
        public WebserviceClient.DistantPrinter.StatusRequestBody Body;
        
        public StatusRequest() {
        }
        
        public StatusRequest(WebserviceClient.DistantPrinter.StatusRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://127.0.0.1/")]
    public partial class StatusRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int jobId;
        
        public StatusRequestBody() {
        }
        
        public StatusRequestBody(int jobId) {
            this.jobId = jobId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class StatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="StatusResponse", Namespace="http://127.0.0.1/", Order=0)]
        public WebserviceClient.DistantPrinter.StatusResponseBody Body;
        
        public StatusResponse() {
        }
        
        public StatusResponse(WebserviceClient.DistantPrinter.StatusResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://127.0.0.1/")]
    public partial class StatusResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string StatusResult;
        
        public StatusResponseBody() {
        }
        
        public StatusResponseBody(string StatusResult) {
            this.StatusResult = StatusResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PrinterSoapChannel : WebserviceClient.DistantPrinter.PrinterSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PrinterSoapClient : System.ServiceModel.ClientBase<WebserviceClient.DistantPrinter.PrinterSoap>, WebserviceClient.DistantPrinter.PrinterSoap {
        
        public PrinterSoapClient() {
        }
        
        public PrinterSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PrinterSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrinterSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrinterSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebserviceClient.DistantPrinter.PrintResponse WebserviceClient.DistantPrinter.PrinterSoap.Print(WebserviceClient.DistantPrinter.PrintRequest request) {
            return base.Channel.Print(request);
        }
        
        public int Print(int taille, string nom, int copies) {
            WebserviceClient.DistantPrinter.PrintRequest inValue = new WebserviceClient.DistantPrinter.PrintRequest();
            inValue.Body = new WebserviceClient.DistantPrinter.PrintRequestBody();
            inValue.Body.taille = taille;
            inValue.Body.nom = nom;
            inValue.Body.copies = copies;
            WebserviceClient.DistantPrinter.PrintResponse retVal = ((WebserviceClient.DistantPrinter.PrinterSoap)(this)).Print(inValue);
            return retVal.Body.PrintResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebserviceClient.DistantPrinter.StatusResponse WebserviceClient.DistantPrinter.PrinterSoap.Status(WebserviceClient.DistantPrinter.StatusRequest request) {
            return base.Channel.Status(request);
        }
        
        public string Status(int jobId) {
            WebserviceClient.DistantPrinter.StatusRequest inValue = new WebserviceClient.DistantPrinter.StatusRequest();
            inValue.Body = new WebserviceClient.DistantPrinter.StatusRequestBody();
            inValue.Body.jobId = jobId;
            WebserviceClient.DistantPrinter.StatusResponse retVal = ((WebserviceClient.DistantPrinter.PrinterSoap)(this)).Status(inValue);
            return retVal.Body.StatusResult;
        }
    }
}