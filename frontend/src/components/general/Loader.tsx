import '@/src/styles/loaderCircle.css';

export default function LoaderCircle() {
    return (
        <div className="loader-wrapper">
            <div className="loader" />
            <div className="text-2xl font-bold mb-6 loader-text">Loading<span className="dot dot1">.</span>
                <span className="dot dot2">.</span>
                <span className="dot dot3">.</span></div>
        </div>
    )
}