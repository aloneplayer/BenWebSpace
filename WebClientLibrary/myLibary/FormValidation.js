// A generic function for checking to see if an input element has
// had information entered into it
function checkRequired(elem)
{
    if (elem.type == "checkbox" || elem.type == "radio")
        return getInputsByName(elem.name).numChecked;
    else
        return elem.value.length > 0 && elem.value != elem.defaultValue;
}


// Find all input elements that have a specified name (good for finding
// and dealing with checkboxes or radio buttons)
function getInputsByName(name)
{
    // The array of input elements that will be matched
    var results = [];
    // Keep track of how many of them were checked
    results.numChecked = 0;
    // Find all the input elements in the document
    var input = document.getElementsByTagName("input");
    for (var i = 0; i < input.length; i++)
    {
        // Find all the fields that have the specified name
        if (input[i].name == name)
        {
            // Save the result, to be returned later
            results.push(input[i]);
            // Remember how many of the fields were checked
            if (input[i].checked)
                results.numChecked++;
        }
    }
    // Return the set of matched fields
    return results;
}

// Wait for the document to finish loading
window.onload = function ()
{
    // Get the form and watch for a submit attempt.
    document.getElementsByTagName("form")[0].onsubmit = function ()
    {
        // Get an input element to check
        var elem = document.getElementById("age");
        // Make sure that the required age field has been checked
        if (!checkRequired(elem))
        {
            // Display an error and keep the form from submitting.
            alert("Required field is empty – " + "you must be over 13 to use this site.");
            return false;
        }
        // Get an input element to check
        var elem = document.getElementById("name");
        // Make sure that some text has been entered into the name field
        if (!checkRequired(elem))
        {
            // Otherwise display an error and keep the form from submitting
            alert("Required field is empty – please provide your name.");
            return false;
        }
    };
};


// A generic function for checking to see if an input element
// looks like an email address
function checkEmail(elem)
{
    // Make sure that something was entered and that it looks like
    // a valid email address
    return elem.value == '' || /^[a-z0-9_+.-]+\@([a-z0-9-]+\.)+[a-z0-9]{2,4}$/i.test(elem.value);
}
// Get an input element to check
var elem = document.getElementById("email");
// Check to see if the field is valid, or not
if (!checkEmail(elem))
{
    alert("Field is not an email address.");
}


// A generic function for checking to see if an input element has
// a URL contained in it
function checkURL(elem)
{
    // Make sure that some text was entered, and that it's
    // not the default http:// text
    return elem.value == '' || !elem.value == 'http://' ||
    // Make sure that it looks like a valid URL
/^https?:\/\/([a-z0-9-]+\.)+[a-z0-9]{2,4}.*$/.test(elem.value);
}
// Get an input element to check
var elem = document.getElementById("url");
// Check to see if the field is a valid URL
if (!checkURL(elem))
{
    alert("Field does not contain a URL.");
}


// A generic function for checking to see if an input element has
// a Phone Number entered in it
function checkPhone(elem)
{
    // Check to see if we have something that looks like
    // a valid phone number
    var m = /(\d{3}).*(\d{3}).*(\d{4})/.exec(elem.value);
    // If it is, seemingly, valid - force it into the specific
    // format that we desire: (123) 456-7890
    if (m !== null)
    {
        elem.value = "(" + m[1] + ") " + m[2] + "-" + m[3];
    }
    return elem.value == '' || m !== null;
}

// Get an input element to check
var elem = document.getElementById("phone");
// Check to see if the field contains a valid phone number
if (!checkPhone(elem))
{
    alert("Field does not contain a phone number.");
}

// A generic function for checking to see if an input element has
// a date entered into it
function checkDate(elem)
{
    // Make sure that something is entered, and that it
    // looks like a valid MM/DD/YYYY date
    return !elem.value || /^\d{2}\/\d{2}\/\d{2,4}$/.test(elem.value);
}
// Get an input element to check
var elem = document.getElementById("date");
// Check to see if the field contains a valid date
if (!checkDate(elem))
{
    alert("Field is not a date.");
}


//  通用处理
var errMsg = {
    // Checks for when a specified field is required
    required: {
        msg: "This field is required.",
        test: function (obj, load)
        {
            // Make sure that there is no text was entered in the field and that
            // we aren't checking on page load (showing 'field required' messages
            // would be annoying on page load)
            return obj.value.length > 0 || load || obj.value == obj.defaultValue;
        }
    },
    // Makes sure that the field s a valid email address
    email: {
        msg: "Not a valid email address.",
        test: function (obj)
        {
            // Make sure that something was entered and that it looks like
            // an email address
            return !obj.value || /^[a-z0-9_+.-]+\@([a-z0-9-]+\.)+[a-z0-9]{2,4}$/i.test(obj.value);
        }
    },
    // Makes sure the field is a phone number and
    // auto-formats the number if it is one
    phone: {
        msg: "Not a valid phone number.",
        test: function (obj)
        {
            // Check to see if we have something that looks like
            // a valid phone number
            var m = /(\d{3}).*(\d{3}).*(\d{4})/.exec(obj.value);
            // If it is, seemingly, valid - force it into the specific
            // format that we desire: (123) 456-7890
            if (m) obj.value = "(" + m[1] + ") " + m[2] + "-" + m[3];
            return !obj.value || m;
        }
    },
    // Makes sure that the field is a valid MM/DD/YYYY date
    date: {
        msg: "Not a valid date.",
        test: function (obj)
        {
            // Make sure that something is entered, and that it
            // looks like a valid MM/DD/YYYY date
            return !obj.value || /^\d{2}\/\d{2}\/\d{2,4}$/.test(obj.value);
        }
    },
    // Makes sure that the field is a valid URL
    url: {
        msg: "Not a valid URL.",
        test: function (obj)
        {
            // Make sure that some text was entered, and that it's
            // not the default http:// text
            return !obj.value || obj.value == 'http://' ||
            // Make sure that it looks like a valid URL
            /^https?:\/\/([a-z0-9-]+\.)+[a-z0-9]{2,4}.*$/.test(obj.value);
        }
    }
};


// A function for validating all fields within a form.
// The form argument should be a reference to a form element
// The load argument should be a boolean referring to the fact that
// the validation function is being run on page load, versus dynamically
function validateForm(form, load)
{
    var valid = true;
    // Go through all the field elements in form
    // form.elements is an array of all fields in a form
    for (var i = 0; i < form.elements.length; i++)
    {
        // Hide any error messages, if they're being shown
        hideErrors(form.elements[i]);
        // Check to see if the field contains valid contents, or not
        if (!validateField(form.elements[i], load))
        {
            valid = false;
        }
    }
    // Return false if a field does not have valid contents
    // true if all fields are valid
    return valid;
}
// Validate a single field's contents
function validateField(elem, load)
{
    var errors = [];
    // Go through all the possible validation techniques
    for (var name in errMsg)
    {
        // See if the field has the class specified by the error type
        var re = new RegExp("(^|\\s)" + name + "(\\s|$)");
        // Check to see if the element has the class and that it passes the
        // validation test
        if (re.test(elem.className) && !errMsg[name].test(elem, load))
        // If it fails the validation, add the error message to list
            errors.push(errMsg[name].msg);
    }
    // Show the error messages, if they exist
    if (errors.length)
        showErrors(elem, errors);
    // Return false if the field fails any of the validation routines
    return errors.length > 0;
}

// Hide any validation error messages that are currently shown
function hideErrors(elem)
{
    // Find the next element after the current field
    var next = elem.nextSibling;
    // If the next element is a ul and has a class of errors
    if (next && next.nodeName == "UL" && next.className == "errors")
    // Remove it (which is our means of 'hiding')
        elem.parenttNode.removeChild(next);
}

// Show a set of errors messages for a specific field within a form
function showErrors(elem, errors)
{
    // Find the next element after the field
    var next = elem.nextSibling;
    // If the field isn't one of our special error-holders.
    if (next && (next.nodeName != "UL" || next.className != "errors"))
    {
        // We need to make one instead
        next = document.createElement("ul");
        next.className = "errors";
        // and then insert into the correct place in the DOM
        elem.paretNode.insertBefore(next, elem.nextSibling);
    }
    // Now that we have a reference to the error holder UL
    // We then loop through all the error messages
    for (var i = 0; i < errors.length; i++)
    {
        // Create a new li wrapper for each
        var li = document.createElement("li");
        li.innerHTML = errors[i];
        // and insert it into the DOM
        next.appendChild(li);
    }
}

// 提交时进行validation
function watchForm(form)
{
    // Watch the form for submission
    addEvent(form, 'submit', function ()
    {
        // make sure that the form's contents validate correctly
        return validateForm(form);
    });
}
// Find the first form on the page
var form = document.getElementsByTagName("form")[0];
// and watch for when its submitted, to validate it
watchForm(form);