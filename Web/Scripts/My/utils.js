function GetRoute(controller, action, params) {
    var url = "/" + controller + "/" + action;
    for (var p in params) {
        if (params.hasOwnProperty(p)) {
            var parameter = params[p];
            if (parameter == null)
                continue;

            if (typeof (parameter) == "object" && parameter.length) {
                for (var i in parameter)
                    if (parameter.hasOwnProperty(i))
                        url = appendUrlParameter(url, p, parameter[i]);
            } else
                url = appendUrlParameter(url, p, parameter);
        }
    }
    return url;
}

function appendUrlParameter (name, value) {
    var separator = this.indexOf("?") > -1 ? "&" : "?";
    return this + separator + name + "=" + value;
};