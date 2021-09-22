var DansUI = DansUI || {};

DansUI.commander = (function () {
    var self = {};

    self.listen = function (cb) {
        window.addEventListener("keypress", function (e) {
            if (e.shiftKey && e.key == "S") {
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

DansUI.page = (function () {
    var self = {};

    self.currentSelf = function () {
        return {
            Width: document.body.scrollWidth,
            Height: document.body.scrollHeight
        };
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