export const Loading = () => {
    return (
        <div className="flex flex-col items-center justify-center py-24 gap-4">
            <div className="flex gap-2">
                <div className="w-2.5 h-2.5 bg-indigo-500 rounded-full animate-bounce [animation-delay:0ms]" />
                <div className="w-2.5 h-2.5 bg-indigo-500 rounded-full animate-bounce [animation-delay:150ms]" />
                <div className="w-2.5 h-2.5 bg-indigo-500 rounded-full animate-bounce [animation-delay:300ms]" />
            </div>
            <p className="text-slate-400 text-sm">Učitavanje...</p>
        </div>
    );
}