function scrollTo(element = document.documentElement, to = 0, duration = 200) {
    if (duration <= 0) {
        return;
    }

    const el = element;
    const difference = to - el.scrollTop;
    const perTick = (difference / duration) * 10;
    setTimeout(() => {
        el.scrollTop += perTick;

        if (el.scrollTop === to) {
            return;
        }

        scrollTo(el, to, duration - 10);
    }, 10);
}

export default scrollTo;

export { scrollTo };
