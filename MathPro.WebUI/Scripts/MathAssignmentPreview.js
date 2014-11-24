(function () {
    var QUEUE = MathJax.Hub.queue;  // shorthand for the queue
    var math = null, box = null;    // the element jax for the math output, and the box it's in

    //
    //  Hide and show the box (so it doesn't flicker as much)
    //
    var HIDEBOX = function () { box.style.visibility = "hidden" }
    var SHOWBOX = function () { box.style.visibility = "visible" }

    //
    //  Get the element jax when MathJax has produced it.
    //
    QUEUE.Push(function () {
        math = MathJax.Hub.getAllJax("MathOutputAssignment")[0];
        box = document.getElementById("box");
        SHOWBOX(); // box is initially hidden so the braces don't show
    });

    //
    //  The onchange event handler that typesets the math entered
    //  by the user.  Hide the box, then typeset, then show it again
    //  so we don't see a flash as the math is cleared and replaced.
    //
    window.UpdateMathAssignment = function (TeX) {
        QUEUE.Push(HIDEBOX, ["Text", math, "\\displaystyle{" + TeX + "}"], SHOWBOX);
    }
})();

(function () {
    var QUEUE = MathJax.Hub.queue;  // shorthand for the queue
    var math = null, box = null;    // the element jax for the math output, and the box it's in

    //
    //  Hide and show the box (so it doesn't flicker as much)
    //
    var HIDEBOX = function () { box.style.visibility = "hidden" }
    var SHOWBOX = function () { box.style.visibility = "visible" }

    //
    //  Get the element jax when MathJax has produced it.
    //
    QUEUE.Push(function () {
        math = MathJax.Hub.getAllJax("MathOutputAnswer")[0];
        box = document.getElementById("box");
        SHOWBOX(); // box is initially hidden so the braces don't show
    });

    //
    //  The onchange event handler that typesets the math entered
    //  by the user.  Hide the box, then typeset, then show it again
    //  so we don't see a flash as the math is cleared and replaced.
    //
    window.UpdateMathAnswer = function (TeX) {
        QUEUE.Push(HIDEBOX, ["Text", math, "\\displaystyle{" + TeX + "}"], SHOWBOX);
    }
})();