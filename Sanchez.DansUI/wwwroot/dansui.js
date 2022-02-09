var DansUI = DansUI || {};

DansUI.commander = (function () {
    var self = {};

    self.listen = function (cb) {
        window.addEventListener("keypress", function (e) {
            if (e.key === "/") {
                if (document.activeElement.tagName.toLowerCase() != "input") {
                    e.stopPropagation();
                    e.preventDefault();
                    cb.invokeMethodAsync("CommanderTriggered")
                        .catch(function (error) {
                            console.error("Error triggering the commander", error);
                        });
                }
            }
        }, false);
    }

    return self;
})();

DansUI.controls = (function () {
    var self = {};

    self.focus = function (element) {
        return element.focus();
    }

    self.blur = function (element) {
        return element.blur();
    }

    return self;
})();

DansUI.dropper = (function () {
    var self = {};

    self.getBoundingBox = function (element) {
        return element.getBoundingClientRect();
    }

    return self;
})();

DansUI.dragger = (function () {
    var self = {};

    self.bindDraggable = function (element, item) {
        function handleDragStart(event) {
            event.dataTransfer.dropEffect = "move";
            event.dataTransfer.setData("text/plain", item);
        }
        element.addEventListener("dragstart", handleDragStart);
    }

    self.bindDroppable = function (element, onDragOver, onDragLeave) {
        function handleDragOver(event) {
            // fired when dropped
            console.log("dragover");
        }
        //element.addEventListener("dragover", handleDragOver);

        function handleDragEnter(event) {
            // fired when entered the zone
            console.log("dragenter");
            var dragItems = event.dataTransfer.items;
            for (var i = 0; i < dragItems.length; i++) {
                if ((dragItems[i].kind == 'string') && (dragItems[i].type.match('^text/plain'))) {
                    dragItems[i].getAsString(function (s) {
                        console.log(s);
                    });
                }
            }
        }
        element.addEventListener("dragenter", handleDragEnter);

        function handleDragLeave(event) {
            // fired when left the zone
            console.log("dragleave");
        }
        element.addEventListener("dragleave", handleDragLeave);

        function handleDropped(event) {
            // fired when dropped on
            console.log("I got dropped on");
        }
        element.addEventListener("drop", handleDropped);
    }

    self.handleDragStart = function (serializedData, event) {
        console.log("Here");
        console.log(serializedData);
        event.dataTransfer.dropEffect = "move";
        event.dataTransfer.setData("text/plain", serializedData);

    }

    return self;
})();

DansUI.input = (function () {
    var self = {};

    self.bindFileInput = function (dropZone, inputFile) {
        function onDragHover(e) {
            e.preventDefault();
            e.dataTransfer.dropEffect = "link";
        }

        function onDragLeave(e) {
            e.preventDefault();
        }

        function onDrop(event) {
            event.preventDefault();
            event.stopPropagation();

            inputFile.files = event.dataTransfer.files;
            const changeEvent = new Event("change", { bubbles: true });
            inputFile.dispatchEvent(changeEvent);
        }

        dropZone.addEventListener("dragenter", onDragHover);
        dropZone.addEventListener("dragover", onDragHover);
        dropZone.addEventListener("dragleave", onDragLeave);
        dropZone.addEventListener("drop", onDrop);

        return {
            dispose: () => {
                dropZone.removeEventListener("dragenter", onDragHover);
                dropZone.removeEventListener("dragover", onDragHover);
                dropZone.removeEventListener("dragleave", onDragLeave);
                dropZone.removeEventListener("drop", onDrop);
            }
        }
    }

    return self;
})();

DansUI.page = (function () {
    var self = {};

    self.currentSelf = function () {
        return {
            Width: document.body.scrollWidth,
            Height: document.body.scrollHeight
        };
    }

    self.triggerFileDownload = function (fileName, url) {
        const anchorElement = document.createElement("a");
        anchorElement.href = url;
        anchorElement.download = fileName ?? "";
        anchorElement.click();
        anchorElement.remove();
    }

    self.downloadFileFromStream = async function (fileName, contentStreamReference) {
        const arrayBuffer = await contentStreamReference.arrayBuffer();
        const blob = new Blob([arrayBuffer]);
        const url = URL.createObjectURL(blob);

        self.triggerFileDownload(fileName, url);

        URL.revokeObjectURL(url);
    }

    return self;
})();

DansUI.styles = (function () {
    var self = {};

    let styledElement = undefined;

    self.setStyleTheme = function (content) {
        if (styledElement === undefined) {
            const element = document.createElement("style");
            document.head.appendChild(element);

            styledElement = element;
        }

        styledElement.innerHTML = content;
    }

    return self;
})();

DansUI.window = (function () {
    var self = {};

    self.currentSize = function () {
        return {
            Width: window.innerWidth,
            Height: window.innerHeight
        };
    }

    self.resize = function (cb) {
        window.addEventListener("resize", function () {
            cb.invokeMethodAsync("UpdateBrowserDimensions", window.innerWidth, window.innerHeight)
                .catch(function (error) {
                    console.error("Error updating the browser resize: ", error);
                });
        });
        return "hello";
    }

    return self;
})();