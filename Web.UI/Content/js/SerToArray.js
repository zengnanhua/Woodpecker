$.fn.serobject = function () {
    var o = {};
    var fields = this.serializeArray();
    $.each(fields, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};


function format(n, sep, decimals) {
    sep = sep || "."; // Default to period as decimal separator
    decimals = decimals || 2; // Default to 2 decimals
    return n.toLocaleString().split(sep)[0]
     + sep
    + n.toFixed(decimals).split(sep)[1];
}


function html2Escape(sHtml) {
    return sHtml.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;' }[c]; });
}
