$("#text-draftbot-intro-1").delay(500).animate({ opacity: 1 }, 400);
$("#text-draftbot-intro-2").delay(1500).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-1").delay(1750).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-2").delay(2000).animate({ opacity: 1 }, 400);
$("#buttons-draftbot-intro-3").delay(2250).animate({ opacity: 1 }, 400);

$('#myTabs a').click(function (e) {
    e.preventDefault()
    $(this).tab('show')
})