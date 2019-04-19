// Global vars to fill in manually
var currentWeight = "205.1 lbs. (+1.2 slwi| 29.15%bf). ";

function onOpen() {
  /*
  
  Create Menu and Load on start in doc
  
  */
    "use strict";
    
    var ui = DocumentApp.getUi();     // Or DocumentApp or FormApp.

    ui.createMenu('Glif Journal Plus+')
        .addItem('Insert Day Title', 'insertSpecialDayTitle')
        .addItem('Insert Date', 'insertCurrentDate')
        .addItem('Insert Countdown', 'insertCountdown')
        .addItem('Insert Time Entry', 'insertCurrentTime')
        .addSeparator()
        .addItem('Insert RandomVerse', 'insertRandomVerse')
        .addItem('Whole Shebang', 'InsertJournalTotalStart')
        .addToUi();
}


function insertCurrentDate() {
  /*
  
  Insert current date in journal subheading format. saves time looking up
  
  */
    "use strict";
    
    var dateSize = 8,
        dateColor = "#666666",
        d = new Date(),
        n = d.getDate(),
        y = d.getFullYear(),

        // add symbol at the cursor position
        cursor = DocumentApp.getActiveDocument().getCursor(),
        today = new Date(),
  
        //Get the month and days of the week based on index value
        // const var monthNames = ["January", ...
        monthNames = ["January", "February", "March", "April", "May", "June", "July",
                              "August", "September", "October", "November", "December"],
  
        //const var dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],

        style = {},
        element = cursor.insertText(monthNames[d.getMonth()] + " " + n + ", " + y + ': ' + dayNames[d.getDay()]),
        parent = element.editAsText().getParent();
    
    style[DocumentApp.Attribute.HEADING] = DocumentApp.ParagraphHeading.NORMAL;

    parent.setAttributes(style);
    element.setFontSize(dateSize);
    element.setBold(true);
    element.setForegroundColor(dateColor);
    setCursorPosEnd(element);
}


function insertCountdown() {
  /*
  
  Countdown from days start to keep from having to count all the time and edit when I place the entry
  
  */
  
  //note: monthly adjustments 
  //Feb + 3
  //Mar - 3
    "use strict";
    var doc = DocumentApp.getActiveDocument(),
        cursor = DocumentApp.getActiveDocument().getCursor(),
        journalStart = countTimeToString(countDaysFrom([2017, 5, 30])),
        caliStart = countTimeToString(countDaysFrom([2006, 6, 21])),
        lostLegacy = countTimeToString(countDaysFrom([2017, 7, 14])),
        comp = countTimeToString(countDaysFrom([2017, 8, 17])),
        miami = (countDaysFrom([2018, 12, 7])),
        comicomp = countTimeToString(countDaysFrom([2018, 1, 7])),
        monarch = countTimeToString(countDaysFrom([2018, 2, 4]) - 3),
        monarcomp = countTimeToString(countDaysFrom([2018, 2, 20]) - 3),
        workout = countTimeToString(countDaysFrom([2018, 2, 27]) - 3),
        novelstart = countTimeToString(countDaysFrom([2018, 3, 8])),
        novelcomp = (countDaysFrom([2018, 8, 19]) - 1),
        resign = (countDaysFrom([2019,2,28]) - 4),
        fullCountdownString = (journalStart + " since journal start. " + caliStart + " since California start. " + lostLegacy + " since Lost Legacy. " + comp + " since comp. "
                                + comicomp + " since Comicomp. " + monarch + " since the start of Monarch 2.0, " + monarcomp + " since Monarcomp. " + currentWeight + workout +
                                " since working out again. " + novelstart + " since compass novel start. " + novelcomp + " days since novel/crpg comp. " + miami + " days since Miami. " + 
                                resign + " days since NDI retirement."),
  
        style = {},
        styleTitle = {},
        monthNames,
        d, dayTitle,
        element;
    
    style[DocumentApp.Attribute.FOREGROUND_COLOR] = '#000000';
    style[DocumentApp.Attribute.HEADING] = DocumentApp.ParagraphHeading.NORMAL;
    style[DocumentApp.Attribute.FONT_SIZE] = 11;
    style[DocumentApp.Attribute.BOLD] = false;
    style[DocumentApp.Attribute.HORIZONTAL_ALIGNMENT] = DocumentApp.HorizontalAlignment.LEFT;
  
    //note: meant for styleTitle but looks like it's not used
    style[DocumentApp.Attribute.BOLD] = true;

    //const monthNames = ["SUPER SUNDAY",...
    monthNames = ["SUPER SUNDAY", "MUOO MONDAY", "TAPPY TUESDAY", "WACKY WEDNESDAY", "THIRSTY THURSDAY", "FINALLY FRIDAY", "SHABBAT SHALOM!"];

    //const d = new Date();
    d = new Date();
    dayTitle = monthNames[d.getDay()];

    element = cursor.insertText(dayTitle + "\n");
    element.setAttributes(style);
    element.setBold(true);
    setCursorPosEnd(element);

    element = cursor.insertText(fullCountdownString + "\n");
    element.setBold(false);
    setCursorPosEnd(element);

    insertText("\n");
    
    //inserts random verse
    insertRandomVerse();

    insertText("\n\n", "normal");
}


function insertCurrentTime() {
  /*
  
  Inserts current time for typical journal entry. Very common to use, saves time to look up time
  
  */
    "use strict";
    var doc = DocumentApp.getActiveDocument(),
        cursor = doc.getCursor(),
        d = new Date(),
        h = d.getHours(), // => 9
        m = (d.getMinutes() < 10 ? '0' : '') + d.getMinutes(),
        ampm = (d.getHours() >= 12) ? "p" : "a",
        currentTime,
        element;

    if (h > 12) { h = h - 12; }
    if (h === 0) { h = 12; }
  
    currentTime = h + ":" + m + ampm + " - ";

    element = cursor.insertText(currentTime);
    element.setBold(true);
  
    setCursorPosEnd(element);
    element = cursor.insertText("");
    element.setBold(false);
}


function insertSpecialDayTitle() {
/*

    Insert my own special naming for days, saves time calculating tricky naming to remind me that every day is special
    and deserves it's own name for it ;)

*/
    "use strict";
    
    var doc = DocumentApp.getActiveDocument(),
        cursor = doc.getCursor(),
        d = new Date(),
        dayFullName,
        month,
        style,
        element,
        monthNames,
        weekNames,
        dayNames;
  
    //const monthNames = ["JA", "FE", "MA", ...
    monthNames = ["JA", "FE", "MA", "AP", "MY", "JUN",
                  "JUL", "AU", "SE", "OC", "NO", "DE"];
  
    //const weekNames = ["UN", "ON", "UE", "ED", "UR", "RI",...
    weekNames = ["UN", "ON", "UE", "ED", "UR", "RI", "SHA"];
  
    //const dayNames = ["", "FI", "SE", "THI", "FO", "FI", "SI", "SEV","EI","NI","TE",...                    
    dayNames = ["", "FI", "SE", "THI", "FO", "FI", "SI", "SEV", "EI", "NI", "TE",
                "EL", "TWE", "THITE", "FOTE", "FITE", "SITE", "SETE", "EITE", "NITE", "TWETE",
                "TWEFI", "TWESE", "TWETHI", "TWEFO", "TWEEFI", "TWESI", "TWESEV", "TWEEI", "TWENI", "THITE",
                "THIFI", "THISE"];
                    
    dayFullName = monthNames[d.getMonth()] + weekNames[d.getDay()] + dayNames[d.getDate()];
    if (d.getDay() === 6) { dayFullName = weekNames[d.getDay()] + monthNames[d.getMonth()] + dayNames[d.getDate()]; }
  
    //if dates for seasons are detected, announce them!
    month = d.getMonth() + 1;
    if (d.getDate() === 20 && month === 3) {dayFullName = '!SPRING BEGINS!'; }
    if (d.getDate() === 21 && month === 6) {dayFullName = '!SUMMER BEGINS!'; }
    if (d.getDate() === 22 && month === 9) {dayFullName = '!FALL BEGINS!'; }
    if (d.getDate() === 21 && month === 12) {dayFullName = '!WINTER BEGINS!'; }

    style = {};
    style[DocumentApp.Attribute.HEADING] = DocumentApp.ParagraphHeading.TITLE;
    style[DocumentApp.Attribute.FONT_SIZE] = 36;
    style[DocumentApp.Attribute.BOLD] = true;
    style[DocumentApp.Attribute.HORIZONTAL_ALIGNMENT] = DocumentApp.HorizontalAlignment.CENTER;
  
    element = cursor.insertText(dayFullName);
    element.setAttributes(style);
    setAtrributes(element, style);
    setCursorPosEnd(element);
}

function insertRandomVerse() {
/*

  Fetch json data from ourmanna.com website at random and insert text into document. 

*/
    "use strict";
    
    var url = "https://beta.ourmanna.com/api/v1/get/?format=json&order=random",
        data = getJSONfromUrl(url),
        verse = data.verse.details.text,
        chapter = data.verse.details.reference;
    insertText(verse + " " + chapter, "italic");
    insertText(" ");
}

function test() {
    Logger.log("this and that");
}

function InsertJournalTotalStart() {
/*

    WHOLE SHEBANG

*/
    "use strict";
  
    //TITLE INSERT
    var doc = DocumentApp.getActiveDocument(),
        cursor = doc.getCursor(),
        d = new Date(),
        monthNames,
        weekNames,
        dayNames,
        dayFullName,
        month,
        style,
        element,
        dateSize, dateColor,
        n, y,
        today,
        monthNames2, dayNames2;
  
    //const monthNames = ["JA", "FE", "MA", "AP", "MY", "JUN",...
    monthNames = ["JA", "FE", "MA", "AP", "MY", "JUN", "JUL", "AU", "SE", "OC", "NO", "DE"];
  
    //const weekNames = ["UN", "ON", "UE", "ED", "UR", "RI",...
    weekNames = ["UN", "ON", "UE", "ED", "UR", "RI", "SHA"];
  
    //const dayNames = ["", "FI", "SE", "THI", "FO", "FI", "SI", "SEV","EI","NI","TE",...
    dayNames = ["", "FI", "SE", "THI", "FO", "FI", "SI", "SEV", "EI", "NI", "TE",
                    "EL", "TWE", "THITE", "FOTE", "FITE", "SITE", "SETE", "EITE", "NITE", "TWETE",
                    "TWEFI", "TWESE", "TWETHI", "TWEFO", "TWEEFI", "TWESI", "TWESEV", "TWEEI", "TWENI", "THITE",
                    "THIFI", "THISE"];
                    
    dayFullName = monthNames[d.getMonth()] + weekNames[d.getDay()] + dayNames[d.getDate()];
    if (d.getDay() == 6) {dayFullName =  weekNames[d.getDay()] + monthNames[d.getMonth()] + dayNames[d.getDate()]; }
  
    //if dates for seasons are detected, aannounce them!
    month = d.getMonth() + 1;
    if (d.getDate() == 20 && month == 3) {dayFullName = '!Spring Begins!'; }
    if (d.getDate() == 21 && month == 6) {dayFullName = '!Summer Begins!'; }
    if (d.getDate() == 22 && month == 9) {dayFullName = '!Fall Begins!'; }
    if (d.getDate() == 21 && month == 12) {dayFullName = '!Winter Begins!'; }

    style = {};
    style[DocumentApp.Attribute.HEADING] = DocumentApp.ParagraphHeading.TITLE;
    style[DocumentApp.Attribute.FONT_SIZE] = 36;
    style[DocumentApp.Attribute.BOLD] = true;
    style[DocumentApp.Attribute.HORIZONTAL_ALIGNMENT] = DocumentApp.HorizontalAlignment.CENTER;
  
    element = cursor.insertText(dayFullName);
    element.setAttributes(style);
    setAtrributes(element, style);
    setCursorPosEnd(element);
   
    element = cursor.insertText("\n");
    setCursorPosEnd(element);
  
    //DATE INSERT
    dateSize = 8;
    dateColor = "#666666";
    //const d = new Date();
    n = d.getDate();
    y = d.getFullYear();
  
    // add symbol at the cursor position
    //var cursor = DocumentApp.getActiveDocument().getCursor();
    today = new Date();
  
    //Get the month and days of the week based on index value
    monthNames2 = ["January", "February", "March", "April", "May", "June", "July",
                   "August", "September", "October", "November", "December"];
  
    dayNames2 = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

    style = {};
    //style[DocumentApp.Attribute.HEADING] = DocumentApp.ParagraphHeading.SUBTITLE;
    //style[DocumentApp.Attribute.FONT_SIZE] = 8;
    //style[DocumentApp.Attribute.BOLD] = true;
    style[DocumentApp.Attribute.HORIZONTAL_ALIGNMENT] = DocumentApp.HorizontalAlignment.LEFT


    element = cursor.insertText(monthNames2[d.getMonth()] + " " + n + ", " + y + ': ' + dayNames2[d.getDay()]);
  
    //setting style on parent is VERY powerful and overrides everything for the doc. Too strong for automation.
    //parent = element.editAsText().getParent()
    //parent.setAttributes(style);
    element.setFontSize(dateSize);
    element.setBold(true);
    element.setForegroundColor(dateColor);

    setCursorPosEnd(element);
    //insertCountdown();
}

// EXTRA FUNCTIONS ----------------------------------

function getJSONfromUrl(url) {
/*

    Fetch json data from url and return data. 

    url = (string) web dress of json data
    return = (JSON) data to parse into string

    example: 
       //print text from json data
       var data = getJSONfromUrl(url);
       var verse = data.verse.details.text;
*/
    "use strict";
    
    var response = UrlFetchApp.fetch(url),
        data = JSON.parse(response);
    
    //Logger.log(data);

    return data;
}

function countDaysFrom(startDate) {
/*
  
  Counts days from a certain date, used often with countdown
  
  return (int) = days diff between start date and current time

*/
    //April = +2

    "use strict";
    var oneDay = 24 * 60 * 60 * 1000, // hours*minutes*seconds*milliseconds
        firstDate = new Date(startDate[0], startDate[1], startDate[2]),
        date = new Date(),
        day = date.getDate(),
        month = date.getMonth() + 1,
        year = date.getFullYear(),
        secondDate = new Date(year, month, day),

        diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay))) + 2;
    
    return diffDays;
}

function countTimeToString(startDate) {
/*
  
    Takes the days count and adds years if past 365, returns string with days or years depending
    if less than 365.

    return (int) = days diff between start date and current time
  
*/
    "use strict";
    var timePass = '',
        years = Math.floor((startDate / 365)),
        yearString = 'year';
  
    if(years > 1){ yearString = 'years'; }
    
    if (startDate > 365) { timePass = years.toString() + yearString + ' and ' + (startDate - (365 * years)).toString() + ' days'; }
    else { timePass = startDate.toString() + ' days'; }
    
    return timePass;
}

function setCursorPosEnd(element) {
/* 

    Sets cursor to the end of a sentence

    doc = (object) the document to which the script is container-bound.
    element = (string) cursor text used to move position toward end
    
*/
    "use strict";
    
    var doc = DocumentApp.getActiveDocument(),
        position = doc.newPosition(element, element.getText().length);
    
    doc.setCursor(position);
}


function setAtrributes(element, style) {
/*
  
    Forces set attributes to cursor text by forcing unsupported edits such as alignment
    and jusitfy by appeending changes to the cursor text's parent.

    element = (string) cursor text used to move position toward end
    style = (dictionary) key, value of DocumentApp.Attributes such as HEADING, FONT_SIZE, BOLD
  
*/
    "use strict";
    
    var parent = element.editAsText().getParent();
    parent.setAttributes(style);
}


function insertText(text, style) {
/*

    Simple way to insert text into google doc app.

    text =(string) any string sentence to be printed in google doc
    style = (string) "bold", "italic", or "" to set the style type for text

*/
    "use strict";
    
    var cursor = new Cursor(),
        element = cursor.insertText(text);

    //sets style based on string input
    setElementStyle(element, style, true);

    //set cursor end
    setCursorPosEnd(element);

    //set a space to return style to default
    element = cursor.insertText(" ");

    // returns text back to normal after cursor move
    setElementStyle(element, style, false);

    return element;
}

function setElementStyle(element, style, enable) {
/* 
  
    Sets style of text element to bold, italc, or default in one simple command

    element = (string) cursor text used to move position toward end
    style = (string) "bold", "italic", or "" to set the style type for element
    enable = (bool) true: enables the style type. false:disable the style type
    
*/
    "use strict";
    
    if (style == "bold") {
        element.setBold(enable);

    } else if (style == "italic") {
        element.setItalic(enable);

    } else {
        //do nothing 
    }
}

function Cursor() {
/*
  
    Gets the document cursor in one line
  
*/
    "use strict";
    
    var doc = DocumentApp.getActiveDocument(),
        cursor = doc.getCursor();
    
    return cursor;
}

// EXPERIMENTAL  ------------------------------------

function insertFullHeader() {
/*
  
    Attempt to paste full header into journal
  
*/
    "use strict";
    
    var doc = DocumentApp.getActiveDocument(),
        cursor = doc.getCursor(),
        element;

    insertCountdown();
    element = cursor.insertText('\n');
    insertDate();
    insertSpecialDayTitle();
}


function Day() {
    "use strict";
    
    var date = new Date(),
        day = date.getDate();
    
    return day;
}


function Month() {
    "use strict";
    
    var date = new Date(),
        month = date.getMonth() + 1;
    
    return month;
}


function Year() {
    "use strict";
    
    var date = new Date(),
        year = date.getFullYear();
    
    return year;
}


function Time(format) {
    "use strict";
    
    // hh:mm:pp
    var d = new Date(),
        h = d.getHours(), // => 9
        m = (d.getMinutes() < 10 ? '0' : '') + d.getMinutes(),
        ampm = (d.getHours() >= 12) ? "p" : "a",
        currentTime;

    if (h > 12) { h = h - 12; }
    if (h == 0) { h = 12; }

    currentTime = h + ':' + m + ampm;

    return currentTime;
}


function paste() {
/*

    Test to stream or get content from a random scripture site

*/
    "use strict";
    /*
    var doc = DocumentApp.getActiveDocument()
    var cursor = doc.getCursor();

    var options = {
    'method' : 'post',
    'contentType': 'multipart/form-data'
    };

    var url = "http://www.ccesonline.com/quoter.js";
    url = "http://www.kingjamesbibleonline.org/popular-bible-verses-widget.php";

    var javascript = UrlFetchApp.fetch(url, options);
    Logger.log(javascript);

    var element = cursor.insertText(javascript);
    */

    insertText(Time('hh:mm'));
  
}


//TODO
//Needs full cleanup
//Globa vars to update