var options = { iframe: { url: 'upload.php' } };
var zone = new FileDrop('zbasic', options);

zone.event('send', function (files) {
    files.each(function (file) {
        file.event('done', function (xhr) {
            alert('Done uploading ' + this.name + ',' +
                  ' response:\n\n' + xhr.responseText);
        });

        file.event('error', function (e, xhr) {
            alert('Error uploading ' + this.name + ': ' +
                  xhr.status + ', ' + xhr.statusText);
        });

        file.sendTo('upload.php');
    });
});

// <iframe> uploads are special - handle them.
zone.event('iframeDone', function (xhr) {
    alert('Done uploading via <iframe>, response:\n\n' + xhr.responseText);
});

// Toggle multiple file selection in the File Open dialog.
fd.addEvent(fd.byID('zbasicm'), 'change', function (e) {
    zone.multiple((e.currentTarget || e.srcElement).checked);
});
